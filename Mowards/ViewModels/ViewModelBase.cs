using System;
namespace Mowards.ViewModels
{
    public abstract class ViewModelBase : MutableDataObject
    {
        #region Common Functions

        protected abstract void InitCommands();

        protected abstract void InitClass();
        #endregion
    }
}
