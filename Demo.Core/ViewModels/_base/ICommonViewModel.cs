using MvvmCross.Core.ViewModels;

namespace Demo.Core.ViewModels
{
    public interface ICommonViewModel : IMvxViewModel
    {
        bool Loading { get; set; }

        NavigationHint Hint { get; }

        void Unbind();
    }
}

