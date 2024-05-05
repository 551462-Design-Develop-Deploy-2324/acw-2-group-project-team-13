window.getCurrentPositionWrapper = function (dotnetHelper) {
    navigator.geolocation.getCurrentPosition(function (position) {
        dotnetHelper.invokeMethodAsync('ShowPosition', position);
    });
};