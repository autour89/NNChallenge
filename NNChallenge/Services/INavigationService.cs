using NNChallenge.Constants;

namespace NNChallenge.Services;

public interface INavigationService
{
    void NavigateTo<TParameter>(ScreenType screenType, TParameter parameter)
        where TParameter : class;

    void NavigateTo(ScreenType screenType);

    void GoBack();
}
