using FluentValidation;
using GalaSoft.MvvmLight;
using System;
using System.ComponentModel;
using System.Linq;
/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoChat.Base
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/10 17:41:02													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoChat.Data
{
    public abstract class ValidObservableObject<T> : ObservableObject, IDataErrorInfo
    {
        private ViewModelState _state;
        public ValidObservableObject(IValidator<T> validator,ViewModelState initState = ViewModelState.Original)
        {
            _validator = validator;

            PropertyChanged += (s, e) =>
            {
                AddState(ViewModelState.Updated);
            };
        }
        #region Pub Method

        public ViewModelState State
        {
            get => _state;
            set => Set(ref _state, value); 
        }

        protected void AddState(ViewModelState state) => State = State | state;
        protected void RemoveState(ViewModelState state)
        {
            State &= ~state;
        }
        #endregion

        #region IData 

        private IValidator<T> _validator;
        public string this[string columnName]
        {
            get
            {
                var result = columnName.ValidateProperty(this, _validator);
                IsValid = result == string.Empty;
                return result; 
            }
        }

        public bool IsValid
        {
            get
            {
                var result = _validator.Validate(this).IsValid;
                IsValid = result;
                return result;
            }
            set
            {
                if (value)
                {
                    AddState(ViewModelState.Valid);
                }
                else
                    RemoveState(ViewModelState.Valid);
            }
        }

        public string Error
        {
            get {
                var result = _validator.Validate(this);
                return result.Errors.CreateErrorMessage();
            }
        }

        public string[] Errors
        {
            get
            {
                var result = _validator.Validate(this);
                return result.Errors.Select(a => a.ErrorMessage).ToArray();
            }
        }
        #endregion
    }
}
