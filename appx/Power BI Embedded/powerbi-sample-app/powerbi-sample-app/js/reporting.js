function renderReport(containerElement, reportId) {
    $.ajax('/dashboard/details/' + reportId)
        .done(function (data, textStatus, jqXHR) {
            var iFrame = containerElement.append(
                '<iframe class="powerbi-report" frameborder="0" src="' + data.Report.EmbedUrl + '"></iframe>'
            );
            iFrame.on('load', function () {
                var message = { action: 'loadReport', accessToken: data.AccessToken };
                iFrame.contentWindow.postMessage(JSON.stringify(message), '*');
            })
        });
}