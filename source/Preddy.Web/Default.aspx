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
    <meta name="twitter:title" content="ドクターイエロー運行予測">
    <meta name="twitter:description" content="ドクターイエローに関するつぶやきから次の運行日を予測します。">
    <title>ドクターイエロー運行予測</title>
    <link rel="stylesheet" href="//ajax.aspnetcdn.com/ajax/bootstrap/3.3.7/css/bootstrap.min.css">
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-3.1.1.min.js"></script>
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/bootstrap/3.3.7/bootstrap.min.js"></script>
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/knockout/knockout-3.4.2.js"></script>
    <script type="text/javascript" src="//www.google.com/jsapi"></script>
    <script type="text/javascript" src="//twemoji.maxcdn.com/twemoji.min.js"></script>
    <script type="text/javascript" src="/Scripts/app.js"></script>
    <style type="text/css">
        * { font-family: "Meiryo", "Arial", sans-serif; font-weight: 400; }
        h1 { font-size: 32px; }
        h2 { font-size: 20px; }
        #tweet-log td { padding: 5px; vertical-align: top; }
        #tweet-log a { color: inherit; }
        .twitter { margin: 12px 0; }
        .emoji { height: 1em; width: 1em; margin: 0 0.05em 0 0.1em; vertical-align: -0.1em; }
        .chart { display: table-cell; height: 400px; text-align: center; vertical-align: middle; width: 1140px; }
        .list-group-item { vertical-align: top; }
        .list-group-item > div { display: table-cell; vertical-align: top; }
        .tweet-item-profile-image { padding: 0 10px 0 0; }
        .tweet-item-profile-image img { height: 48px; width: 48px; }
        .tweet-item-header { color: #777777; }
        .tweet-item-header div { display: inline-block; padding: 0 3px 0 0; }
        .tweet-item-media-image { display: none; }
        .tweet-item-media-image img { height: auto; max-width: 100%; }
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
        <h1>ドクターイエロー運行予測</h1>
        <p>ドクターイエローに関するつぶやきから次の運行日を予測します。</p>
        <div class="row">
            <div id="tweet-forecast" class="col-md-12">
                <h2>ツイートの予測</h2>
                <p>今後 30 日のツイートの予測を表示します。</p>
                <div id="chart-forecast" class="chart">
                    <img src="/Assets/loading.gif" />
                </div>
            </div>
        </div>
        <div class="row">
            <div id="tweet-result" class="col-md-12">
                <h2>ツイートの実績</h2>
                <p>過去 30 日のツイートの実績を表示します。</p>
                <div id="chart-result" class="chart">
                    <img src="/Assets/loading.gif" />
                </div>
            </div>
        </div>
        <div class="row">
            <div id="tweet-log" class="col-md-9">
                <h2>ツイートの詳細</h2>
                <div id="tweet-item" data-bind="with: tweetLog">
                    <div data-bind="if: itemExists()">
                        <p><span data-bind="text: selectedDate"></span>のツイートを表示します。</p>
                        <ul class="list-group" data-bind="foreach: itemArray">
                            <li class="list-group-item">
                                <div class="tweet-item-profile-image">
                                    <img data-bind="attr: { src: profileImageUrl, alt: userName }">
                                </div>
                                <div class="tweet-item-content">
                                    <div class="tweet-item-header">
                                        <div><a href="#" data-bind="text: userName, attr: { href: userUrl }"></a></div>
                                        <div><span data-bind="text: screenName"></span></div>
                                        <div><a href="#" data-bind="text: tweetedAt, attr: { href: statusUrl }"></a></div>
                                    </div>
                                    <div class="tweet-item-text"><span data-bind="html: text"></span></div>
                                    <div class="tweet-item-media-image" data-bind="visible: mediaUrl != null">
                                        <img data-bind="attr: { src: mediaUrl, alt: userName }">
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                    <div data-bind="ifnot: itemExists()">
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
    </script>
</body>
</html>
