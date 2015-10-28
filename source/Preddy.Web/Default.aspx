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
    <meta name="twitter:title" content="ドクターイエロー運行予測(beta)">
    <meta name="twitter:description" content="ドクターイエローに関するつぶやきから次の運行日を予測します。">
    <title>ドクターイエロー運行予測(beta)</title>
    <link rel="stylesheet" href="/Content/bootstrap.min.css">
    <script type="text/javascript" src="/Scripts/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="/Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="/Scripts/knockout-3.3.0.js"></script>
    <script type="text/javascript" src="//www.google.com/jsapi"></script>
    <script type="text/javascript" src="//twemoji.maxcdn.com/twemoji.min.js"></script>
    <style type="text/css">
        * { font-family: "Meiryo", "Arial", sans-serif; font-weight: 400; }
        h1 { font-size: 32px; }
        h2 { font-size: 20px; }
        h3 { font-size: 16px; margin: 0; }
        #chart { display: table-cell; text-align: center; vertical-align: middle; height: 400px; width: 1140px; }
        #tweet-item td { padding: 5px; vertical-align: top; }
        #tweet-item a { color: inherit; }
        .emoji { height: 1em; width: 1em; margin: 0 0.05em 0 0.1em; vertical-align: -0.1em; }
        .tweet-item-header * { display: table-cell; padding: 0 3px 0 0; }
        .tweet-item-header div { color: #777777; }
    </style>
</head>
<body>
    <div class="navbar navbar-inverse">
        <div class="container">
            <div class="navbar-header">
                <a class="navbar-brand" href="javascript:void(0)"><span class="glyphicon glyphicon-stats"></span></a>
            </div>
        </div>
    </div>
    <div class="container">
        <h1>ドクターイエロー運行予測(beta)</h1>
        <p>ドクターイエローに関するつぶやきから次の運行日を予測します。</p>
        <div id="tweet-trend">
            <h2>ツイートの傾向</h2>
            <p>過去1か月以内のツイートの合計を表示します。</p>
            <div id="chart">
                <img src="/Assets/loading.gif" />
            </div>
        </div>
        <div id="tweet-detail">
            <h2>ツイートの詳細</h2>
            <div id="tweet-item">
                <div data-bind="visible: tweetExist() == true">
                    <p><span data-bind="text: selectedDate"></span>のツイートを表示します。</p>
                    <ul class="list-group" data-bind="foreach: tweetArray">
                        <li class="list-group-item">
                            <table>
                                <tr>
                                    <td>
                                        <div class="tweet-item-image">
                                            <img data-bind="attr: { src: ProfileImageUrl, alt: UserName }" height="48" width="48">
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tweet-item-header">
                                            <h3><a href="#" data-bind="text: UserName, attr: { href: UserUrl }"></a></h3>
                                            <div><span data-bind="text: ScreenName"></span></div>
                                            <div><a href="#" data-bind="text: TweetedAt, attr: { href: StatusUrl }"></a></div>
                                        </div>
                                        <div class="tweet-item-text"><span data-bind="html: Text"></span></div>
                                    </td>
                                </tr>
                            </table>
                        </li>
                    </ul>
                </div>
                <div data-bind="visible: tweetExist() != true">
                    <p>表示するデータはありません。グラフの点をクリックすると詳細が表示されます。</p>
                </div>
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
            viewModel.minDate.setMonth(viewModel.minDate.getMonth() - 1);
            viewModel.minDate.setDate(viewModel.minDate.getDate() + 1);
            viewModel.maxDate = new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate());
            ko.applyBindings(viewModel);
            function outputChart() {
                var minDate = dateFormatter.formatValue(viewModel.minDate);
                var maxDate = dateFormatter.formatValue(viewModel.maxDate);
                $.ajax({
                    type: "GET",
                    url: encodeURI("/api/chart?maxdate=" + maxDate + "&" + "mindate=" + minDate),
                    timeout: 5000,
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
                    error: function () { },
                });
            }
            function outputTweet() {
                var selectedDate = viewModel.selectedDate();
                $("#tweet-item").hide();
                $.ajax({
                    type: "GET",
                    url: encodeURI("/api/tweet?date=" + selectedDate),
                    timeout: 5000,
                    success: function (json) {
                        viewModel.tweetExist(json.length > 0);
                        viewModel.tweetArray.removeAll();
                        ko.utils.arrayPushAll(viewModel.tweetArray, json);
                        twemoji.parse(document.body);
                        $("#tweet-item").fadeIn();
                    },
                    error: function () {
                        viewModel.tweetExist(false);
                        viewModel.tweetArray.removeAll();
                        $("#tweet-item").fadeIn();
                    },
                });
                window.location.href = window.location.pathname + "#" + encodeURIComponent(selectedDate);
            }
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
        })();
    </script>
</body>
</html>
