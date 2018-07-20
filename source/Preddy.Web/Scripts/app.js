/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../Scripts/typings/bootstrap/index.d.ts" />
/// <reference path="../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../Scripts/typings/google.visualization/google.visualization.d.ts" />
/// <reference path="../Scripts/typings/twemoji/twemoji.d.ts" />
'use strict';
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var Chart = (function () {
    function Chart(elementId) {
        var _this = this;
        this.element = document.getElementById(elementId);
        this.chart = new google.visualization.AreaChart(this.element);
        this.dateFormatter = new google.visualization.DateFormat({ pattern: 'yyyy/MM/dd' });
        this.dataTable = new google.visualization.DataTable();
        google.visualization.events.addListener(this.chart, 'select', function () {
            var selection = _this.chart.getSelection()[0];
            if (selection != null) {
                var value = _this.dataTable.getValue(selection.row, 0);
                var date = _this.dateFormatter.formatValue(value);
                if (_this.selectedDateChanged != null &&
                    _this.selectedDateChanged.onSelectedDateChanged != null) {
                    _this.selectedDateChanged.onSelectedDateChanged(date);
                }
            }
        });
    }
    Chart.prototype.draw = function () {
        var _this = this;
        var minDate = this.dateFormatter.formatValue(this.minDate);
        var maxDate = this.dateFormatter.formatValue(this.maxDate);
        jQuery.ajax({
            type: 'GET',
            url: encodeURI(this.requestUrl + '?maxdate=' + maxDate + '&mindate=' + minDate),
            timeout: 0,
            success: function (json) {
                var option = {
                    allowHtml: true,
                    fontName: "'Meiryo', 'Arial', san-serif",
                    title: "期間: " + minDate + " - " + maxDate,
                    legend: { position: 'none' },
                    chartArea: {
                        width: '90%',
                        height: '80%'
                    },
                    hAxis: {
                        format: 'M/d',
                        gridlines: { count: json.items.length },
                        slantedText: true
                    },
                    vAxis: {}
                };
                _this.maxDate = new Date(json.maxDate);
                _this.minDate = new Date(json.minDate);
                _this.dataTable.addRows(json.items.length);
                _this.dataTable.addColumn('date', '日付');
                _this.dataTable.addColumn('number', '予測');
                _this.dataTable.addColumn('number', '結果');
                jQuery.each(json.items, function (index, element) {
                    _this.dataTable.setValue(index, 0, new Date(element.date));
                    _this.dataTable.setValue(index, 1, element.forecast);
                    _this.dataTable.setValue(index, 2, element.result);
                });
                _this.chart.draw(_this.dataTable, option);
            },
            error: function () {
                jQuery(_this.element).html('問題が発生しました。ページを再読み込みしてください。');
            }
        });
    };
    return Chart;
}());
var TweetChart = (function (_super) {
    __extends(TweetChart, _super);
    function TweetChart() {
        var _this = _super.call(this, 'tweet-chart') || this;
        var nowDate = new Date();
        _this.minDate = new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate());
        _this.minDate.setTime(_this.minDate.getTime() - (24 * 60 * 60 * 1000) * 29);
        _this.maxDate = new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate());
        _this.maxDate.setTime(_this.maxDate.getTime() + (24 * 60 * 60 * 1000) * 30);
        _this.requestUrl = '/api/summary';
        return _this;
    }
    return TweetChart;
}(Chart));
var TweetLog = (function () {
    function TweetLog() {
        var _this = this;
        this.dateFormatter = new google.visualization.DateFormat({ pattern: 'yyyy/MM/dd' });
        this.selectedDate = ko.observable();
        this.itemArray = ko.observableArray();
        this.itemExists = ko.computed(function () { return _this.itemArray().length > 0; });
        if (window.location.hash.length > 0) {
            var value = decodeURIComponent(window.location.hash.replace('#', ''));
            var date = this.dateFormatter.formatValue(new Date(value));
            this.retrive(date);
        }
    }
    TweetLog.prototype.retrive = function (date) {
        var _this = this;
        this.selectedDate(date);
        jQuery('#tweet-log').hide();
        jQuery.ajax({
            type: 'GET',
            url: encodeURI('/api/log?date=' + date),
            timeout: 0,
            success: function (json) {
                _this.itemArray.removeAll();
                ko.utils.arrayPushAll(_this.itemArray, json);
                twemoji.parse(document.body);
                $('.tweet-item-text').each(function () {
                    $(this).html($(this).html().replace(/((http|https|ftp):\/\/[\w?=&.\/-;#~%-]+(?![\w\s?&.\/;#~%"=-]*>))/g, '<a href="$1">$1</a>'));
                });
                $('#tweet-log').fadeIn();
            },
            error: function () {
                _this.itemArray.removeAll();
                $('#tweet-log').fadeIn();
            }
        });
        window.location.href = window.location.pathname + '#' + encodeURIComponent(date);
    };
    TweetLog.prototype.onSelectedDateChanged = function (date) {
        this.retrive(date);
    };
    return TweetLog;
}());
google.load('visualization', '1', { packages: ['corechart'] });
google.setOnLoadCallback(function () {
    var tweetLog = new TweetLog();
    var tweetChart = new TweetChart();
    tweetChart.selectedDateChanged = tweetLog;
    ko.applyBindings({
        tweetLog: tweetLog,
        tweetChart: tweetChart
    });
    tweetChart.draw();
});
