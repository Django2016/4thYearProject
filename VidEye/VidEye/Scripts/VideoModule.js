(function ($, window, document) {
    //get the $ locally scopped
    var VideoID = 0;
    VidModule = (function () {
        function Like(id) {
            AjaxModule.Post('/Profile/Like', { videoId: id }).done(function (result) {
                if (result == "Success") {
                    alert("Like op success");
                }
            });
        }

        function Rate(id, rating) {
            AjaxModule.Post('/Profile/Rate', { videoId: id, rate:rating }).done(function (result) {
                if (result == "Success") {
                    alert("Like op success");
                }
            });
        }

        function Comment() {
            var comment = $("#VideoComment").val();
            if (comment) {
                AjaxModule.Post('/Profile/Comment', { comment: comment, videoId: VideoID }).done(function (result) {
                    $("#badge-" + VideoID).html(result);
                });
            }
        }

        function ShowModal(id) {
            VideoID = id;
            $('#commentModal').modal('show');
        }
        

        //return object literals connected to the private functions
        return {
            Like: Like,
            Comment: Comment,
            ShowModal: ShowModal,
            Rate:Rate
        };
    }());
}(window.jQuery, window, document));