using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyBrowser.UI.WinApp.ViewModel
{
    /// <summary>
    /// 带验证逻辑的ViewModel
    /// 尚未实现异步验证逻辑
    /// </summary>
    public class ValidateViewModel: ViewModel, INotifyDataErrorInfo
    {
        
        private Dictionary<string, Func<object, object>> _propertys = null;
        private Dictionary<string, ValidationAttribute[]> _validators = null;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private bool _enableValidate = true;

        public ValidateViewModel()
        {
            _propertys = this.GetType().GetProperties().Where(x => GetValidations(x).Length != 0).ToDictionary(x => x.Name, x => GetValueGetter(x));
            //_validators = this.GetType().GetProperties().Where(p => GetValidations(p).Length != 0).ToDictionary(p => p.Name, p => GetValidations(p));
        }

        public override void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            base.RaisePropertyChanged(propertyExpression);
            Validate((propertyExpression.Body as MemberExpression).Member.Name);
        }

        public override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.RaisePropertyChanged(propertyName);
            Validate(propertyName);
        }

        public override void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression, T oldValue, T newValue, bool broadcast)
        {
            base.RaisePropertyChanged(propertyExpression, oldValue, newValue, broadcast);
            Validate((propertyExpression.Body as MemberExpression).Member.Name);
        }

        public override void RaisePropertyChanged<T>([CallerMemberName] string propertyName = null, T oldValue = default(T), T newValue = default(T), bool broadcast = false)
        {
            base.RaisePropertyChanged(propertyName, oldValue, newValue, broadcast);
            Validate(propertyName);
        }


        public void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public bool Validate(string propertyName = null)
        {
            if (!_enableValidate)
                return true;

            if (_validators == null)
                _validators = this.GetType().GetProperties().Where(p => GetValidations(p).Length != 0).ToDictionary(p => p.Name, p => GetValidations(p));

            bool result = true;

            if (string.IsNullOrEmpty(propertyName))
            {
                foreach(var property in _propertys)
                {
                    if ((Errors(property.Key)).Length != 0)
                    {
                        result = false;
                        RaiseErrorsChanged(property.Key);
                    }
                }
            }
            else
            {
                if (!_propertys.ContainsKey(propertyName))
                    return result;

                if ((Errors(propertyName) as string[]).Length == 0)
                    return result;

                result = false;
                RaiseErrorsChanged(propertyName);
            }

            return result;
        }

        private ValidationAttribute[] GetValidations(PropertyInfo property)
        {
            return (ValidationAttribute[])property.GetCustomAttributes(typeof(ValidationAttribute), true);
        }

        private Func<object, object> GetValueGetter(PropertyInfo property)
        {
            var instance = Expression.Parameter(typeof(object), "i");
            var cast = Expression.Convert(Expression.Property(Expression.Convert(instance, this.GetType()), property), typeof(object));
            return (Func<object, object>)Expression.Lambda(cast, instance).Compile();
        }

        private string[] Errors(string propertyName)
        {
            if (_validators != null && _propertys.ContainsKey(propertyName))
            {
                var value = _propertys[propertyName](this);
                var errors = _validators[propertyName].Where(v => !v.IsValid(value)).Select(v => v.ErrorMessage).ToArray();
                return errors;
            }

            return new string[0];
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (!EnableValidate)
                return null;

            return Errors(propertyName);
        }

        public bool HasErrors
        {
            get
            {
                if (_validators == null)
                    return false;

                return _validators.Any(x => x.Value != null);
            }
        }

        public bool EnableValidate
        {
            set
            {
                _enableValidate = value;
            }
            get
            {
                return _enableValidate;
            }
        }

    }
}
