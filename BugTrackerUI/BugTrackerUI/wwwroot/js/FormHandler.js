var AJAXGet = function (url, data, headers, successCallback, errorCallback)
{
    $.Get(setAjaxOptions(null, url, data, headers, successCallback, errorCallback));
}

var AJAXPost = function (url, data, headers, successCallback, errorCallback)
{
    $.Post(setAjaxOptions(null, url, data, headers, successCallback, errorCallback))
}

var AJAXRequest = function (method, url, data, headers, successCallback, errorCallback)
{
    $.ajax(setAjaxOptions(method, url, data, headers, successCallback, errorCallback))
} 

var setAjaxOptions = function(method, url, data, headers, successCallback, errorCallback)
{
    var ajaxOptions = {
        url: url,
        contentType: "application/json",
        success: successCallback,
        error: errorCallback
    }

    if (data)
    {
        ajaxOptions.data = JSON.stringify(data);
    }

    if (method)
    {
        ajaxOptions.method = method;
    }

    if (headers)
    {
        ajaxOptions.headers = headers;
    }

    return ajaxOptions;
}