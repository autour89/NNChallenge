using NNChallenge.Constants;

namespace NNChallenge.Services;

public interface INavigationService
{
    void NavigateTo<TParameter>(ScreenType screenType, TParameter? parameter = null)
        where TParameter : class;

    void GoBack();
}
