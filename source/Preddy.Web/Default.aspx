<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="content-type" content="text/html; charset=utf-8">
    <meta http-equiv="x-ua-compatible" content="IE=edge">
    <meta name="author" content="karamem0">
    <meta name="description" content="ドクターイエローに関するつぶやきから次の運行日を予測します。">
    <meta name="keywords" content="ドクターイエロー">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="twitter:card" content="summary">
    <meta name="twitter:site" content="@karamem0">
    <meta name="twitter:title" content="ドクターイエロー運行予測 (beta)">
    <meta name="twitter:description" content="ドクターイエローに関するつぶやきから次の運行日を予測します。">
    <title>ドクターイエロー運行予測 (beta)</title>
    <link rel="stylesheet" href="/Content/bootstrap.min.css">
    <script type="text/javascript" src="/Scripts/jquery-2.2.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="/Scripts/knockout-3.4.0.js"></script>
    <script type="text/javascript" src="//www.google.com/jsapi"></script>
    <script type="text/javascript" src="//twemoji.maxcdn.com/twemoji.min.js"></script>
    <style type="text/css">
        * { font-family: "Meiryo", "Arial", sans-serif; font-weight: 400; }
        h1 { font-size: 32px; }
        h2 { font-size: 20px; }
        #chart { display: table-cell; text-align: center; vertical-align: middle; height: 400px; width: 1140px; }
        #tweet-item td { padding: 5px; vertical-align: top; }
        #tweet-item a { color: inherit; }
        .twitter { margin: 12px 0; }
        .emoji { height: 1em; width: 1em; margin: 0 0.05em 0 0.1em; vertical-align: -0.1em; }
        .list-group-item { vertical-align: top; }
        .list-group-item > div { display: table-cell; vertical-align: top; }
        .tweet-item-image { padding: 0 10px 0 0; }
        .tweet-item-header { color: #777777; }
        .tweet-item-header div { display: inline-block; padding: 0 3px 0 0; }
    </style>
</head>
<body>
    <div class="navbar navbar-inverse">
        <div class="container">
            <div class="navbar-header">
                <a class="navbar-brand" href="/"><span class="glyphicon glyphicon-stats"></span></a>
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav navbar-right">
                    <li class="twitter">
                        <a href="https://twitter.com/share" class="twitter-share-button" data-url="http://preddy.azurewebsites.net/" data-via="karamem0" data-lang="ja" style="display: none;">ツイート</a>
                        <script>!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs); } }(document, 'script', 'twitter-wjs');</script>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="container">
        <h1>ドクターイエロー運行予測 (beta)</h1>
        <p>ドクターイエローに関するつぶやきから次の運行日を予測します。</p>
        <div class="row">
            <div id="tweet-trend" class="col-md-12">
                <h2>ツイートの傾向</h2>
                <p>過去 30 日以内のツイートの合計を表示します。</p>
                <div id="chart">
                    <img src="/Assets/loading.gif" />
                </div>
            </div>
        </div>
        <div class="row">
            <div id="tweet-detail" class="col-md-9">
                <h2>ツイートの詳細</h2>
                <div id="tweet-item">
                    <div data-bind="visible: tweetExist() == true">
                        <p><span data-bind="text: selectedDate"></span>のツイートを表示します。</p>
                        <ul class="list-group" data-bind="foreach: tweetArray">
                            <li class="list-group-item">
                                <div class="tweet-item-image">
                                    <img data-bind="attr: { src: ProfileImageUrl, alt: UserName }" height="48" width="48">
                                </div>
                                <div class="tweet-item-content">
                                    <div class="tweet-item-header">
                                        <div><a href="#" data-bind="text: UserName, attr: { href: UserUrl }"></a></div>
                                        <div><span data-bind="text: ScreenName"></span></div>
                                        <div><a href="#" data-bind="text: TweetedAt, attr: { href: StatusUrl }"></a></div>
                                    </div>
                                    <div class="tweet-item-text"><span data-bind="html: Text"></span></div>
                                </div>
                            </li>
                        </ul>
                    </div>
                    <div data-bind="visible: tweetExist() != true">
                        <p>表示するデータはありません。グラフの点をクリックすると詳細が表示されます。</p>
                    </div>
                </div>
            </div>
            <div id="description" class="col-md-3">
                <h2>このサイトについて</h2>
                <p><a href="https://ja.wikipedia.org/wiki/%E3%83%89%E3%82%AF%E3%82%BF%E3%83%BC%E3%82%A4%E3%82%A8%E3%83%AD%E3%83%BC">ドクターイエロー - Wikipedia</a></p>
                <p>ドクターイエローの運行は 10 日に 1 回程度とされており、そのスケジュールは公開されていません。このサイトでは、Twitter からドクターイエローの目撃情報を集計し、これまでの運行実績から今後の運行予測を行います。</p>
                <p>このサイトについてのお問い合わせは <a href="https://twitter.com/karamem0">@karamem0</a> までお願いします。</p>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        (function () {
            var dateFormatter = null;
            var dataTable = null;
            var chart = null;
            var viewModel = {
                minDate: null,
                maxDate: null,
                selectedDate: ko.observable(),
                tweetExist: ko.observable(false),
                tweetArray: ko.observableArray(),
            };
            var nowDate = new Date();
            viewModel.minDate = new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate());
            viewModel.minDate.setTime(viewModel.minDate.getTime() - (30 * 24 * 60 * 60 * 1000));
            viewModel.maxDate = new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate());
            ko.applyBindings(viewModel);
            function outputChart() {
                var minDate = dateFormatter.formatValue(viewModel.minDate);
                var maxDate = dateFormatter.formatValue(viewModel.maxDate);
                $.ajax({
                    type: "GET",
                    url: encodeURI("/api/chart?maxdate=" + maxDate + "&" + "mindate=" + minDate),
                    timeout: 0,
                    success: function (json) {
                        viewModel.maxDate = new Date(json.MaxDate);
                        viewModel.minDate = new Date(json.MinDate);
                        dataTable.addRows(json.Results.length);
                        dataTable.addColumn('date', '日付');
                        dataTable.addColumn('number', '件数');
                        $.each(json.Results, function (i, e) {
                            dataTable.setValue(i, 0, new Date(e.Key));
                            dataTable.setValue(i, 1, e.Value);
                        });
                        var option = {
                            fontName: "'Meiryo', 'Arial', san-serif",
                            title: "期間: " + minDate + " - " + maxDate,
                            legend: { position: "none" },
                            hAxis: {
                                format: 'MM/dd',
                                gridlines: { count: json.Results.length },
                                slantedText: true,
                            },
                            vAxis: {}
                        };
                        chart = new google.visualization.AreaChart(document.getElementById("chart"));
                        chart.draw(dataTable, option);
                        google.visualization.events.addListener(chart, 'select', function () {
                            var selection = chart.getSelection()[0];
                            if (selection != null) {
                                var value = dataTable.getValue(selection.row, 0);
                                var date = dateFormatter.formatValue(value)
                                viewModel.selectedDate(date);
                                outputTweet();
                            }
                        });
                    },
                    error: function () {
                        $("#chart").html("問題が発生しました。ページを再読み込みしてください。");
                    }
                });
            }
            function outputTweet() {
                var selectedDate = viewModel.selectedDate();
                $("#tweet-item").hide();
                $.ajax({
                    type: "GET",
                    url: encodeURI("/api/tweet?date=" + selectedDate),
                    timeout: 0,
                    success: function (json) {
                        viewModel.tweetExist(json.length > 0);
                        viewModel.tweetArray.removeAll();
                        ko.utils.arrayPushAll(viewModel.tweetArray, json);
                        twemoji.parse(document.body);
                        $(".tweet-item-text").each(function () {
                            $(this).html(
                                $(this).html().replace(
                                /((http|https|ftp):\/\/[\w?=&.\/-;#~%-]+(?![\w\s?&.\/;#~%"=-]*>))/g,
                                '<a href="$1">$1</a>'));
                        });
                        $("#tweet-item").fadeIn();
                    },
                    error: function () {
                        viewModel.tweetExist(false);
                        viewModel.tweetArray.removeAll();
                        $("#tweet-item").fadeIn();
                    }
                });
                window.location.href = window.location.pathname + "#" + encodeURIComponent(selectedDate);
            }
            try {
                google.load("visualization", "1", { packages: ["corechart"] });
                google.setOnLoadCallback(function () {
                    dateFormatter = new google.visualization.DateFormat({ pattern: "yyyy/MM/dd" });
                    dataTable = new google.visualization.DataTable();
                    outputChart();
                    if (window.location.hash.length > 0) {
                        var value = decodeURIComponent(window.location.hash.replace("#", ""));
                        var date = dateFormatter.formatValue(new Date(value))
                        viewModel.selectedDate(date);
                        outputTweet();
                    }
                });
            } catch (ex) {
                $("#chart").html("問題が発生しました。ページを再読み込みしてください。");
            }
        })();
    </script>
</body>
</html>
