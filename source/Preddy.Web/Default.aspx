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
    <link rel="stylesheet" href="//ajax.aspnetcdn.com/ajax/bootstrap/3.4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="/Contents/app.css">
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-3.3.1.min.js"></script>
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/bootstrap/3.4.1/bootstrap.min.js"></script>
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/knockout/knockout-3.4.2.js"></script>
    <script type="text/javascript" src="//www.google.com/jsapi"></script>
    <script type="text/javascript" src="//twemoji.maxcdn.com/twemoji.min.js"></script>
    <script type="text/javascript" src="/Scripts/app.js"></script>
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-md-11">
                <h1><a href="Default.aspx">ドクターイエロー運行予測</a></h1>
                <p>ドクターイエローに関するつぶやきから次の運行日を予測します。</p>
            </div>
            <div class="col-md-1">
                <div class="twitter">
                    <a href="https://twitter.com/share" class="twitter-share-button" data-url="https://preddy.karamme0.jp" data-via="karamem0" data-lang="ja" style="display: none;">ツイート</a>
                    <script type="text/javascript">!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs); } }(document, 'script', 'twitter-wjs');</script>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div id="tweet-summary">
                    <h2>ツイートの予測と結果</h2>
                    <p>前後 30 日のツイートの予測と結果を表示します。</p>
                    <div id="tweet-chart" class="chart">
                        <img src="/Assets/loading.gif" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-9">
                <h2>ツイートの詳細</h2>
                <div id="tweet-log">
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
            </div>
            <div id="description" class="col-md-3">
                <h2>このサイトについて</h2>
                <p><a href="https://ja.wikipedia.org/wiki/%E3%83%89%E3%82%AF%E3%82%BF%E3%83%BC%E3%82%A4%E3%82%A8%E3%83%AD%E3%83%BC">ドクターイエロー - Wikipedia</a></p>
                <p>ドクターイエローの運行は 10 日に 1 回程度とされており、そのスケジュールは公開されていません。このサイトでは、Twitter からドクターイエローの目撃情報を集計し、これまでの運行実績から Azure Machine Learning による今後の運行予測を行います。</p>
                <p>このサイトについてのお問い合わせは <a href="https://twitter.com/karamem0">@karamem0</a> までお願いします。</p>
                <p><a href="PrivacyPolicy.aspx">プライバシー ポリシー</a>について</p>
            </div>
        </div>
    </div>
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-145853296-2"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());
        gtag('config', 'UA-145853296-2');
    </script>
</body>
</html>
