(function ($, window, document) {
    //get the $ locally scopped

    AjaxModule = (function () {
        function GetPartialViewAsync(url, view, data) {
            return $.ajax({
                url: url,
                type: 'GET',
                data: data,
                traditional: true,
                timeout: 60000,
                cache: false,
                success: function (result) {
                    $("#" + view).html('');
                    $("#" + view).html(result);
                }
            });
        }

        function GetManyPartialsAsync(url, data) {
            return $.ajax({
                url: url,
                type: 'POST',
                data: data,
                traditional: true,
                timeout: 60000
            });
        }
      
        function PostData(url, data) {
            return $.ajax({
                url: url,
                type: 'POST',
                data: JSON.stringify(data),
                contentType: "application/json",
                dataType: "json",
                timeout: 60000
            });
        }

        function PostWithAlert(url, data) {
            return $.ajax({
                url: url,
                type: 'POST',
                data: JSON.stringify(data),
                contentType: "application/json",
                dataType: "json",
                async: false,
                timeout: 60000,
                success: function (result) {
                    alert(result);
                    location.reload();
                }
            });
        }

        function GetData(url, data) {           
            return $.ajax({
                url: url,
                type: 'POST',
                data: JSON.stringify(data),
                contentType: "application/json",
                dataType: "json",
                timeout: 60000
            });
        }

        function GetPartialView(url, data, target) {
            return $.post(url, data, function (result) {
                if (result.Json.Success) {
                    $.each(target, function (ind, obj) {
                        $('#' + obj).html(result.Html[ind]);
                    });
                }
                else {
                    alert(result.Json.ReturnValue);
                }
            });
        }

        //return object literals connected to the private functions
        return {
            Get: GetData,
            GetPartial: GetPartialViewAsync,
            Post: PostData,
            PostWithAlertAndReload: PostWithAlert,
            GetManyPartials: GetManyPartialsAsync,
            RenderPartial: GetPartialView
        };

    }());
}(window.jQuery, window, document));