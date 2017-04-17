/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../Scripts/typings/bootstrap/index.d.ts" />
/// <reference path="../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../Scripts/typings/google.visualization/google.visualization.d.ts" />
/// <reference path="../Scripts/typings/twemoji/twemoji.d.ts" />

'use strict';

interface ISelectedDateChanged {

    onSelectedDateChanged(date: string): void;

}

class Chart {

    public selectedDateChanged: ISelectedDateChanged;

    protected element: HTMLElement;
    protected chart: google.visualization.AreaChart;
    protected dataTable: google.visualization.DataTable;
    protected dateFormatter: google.visualization.DateFormat;
    protected requestUrl: string;
    protected minDate: Date;
    protected maxDate: Date;

    protected constructor(elementId: string) {
        this.element = document.getElementById(elementId);
        this.chart = new google.visualization.AreaChart(this.element);
        this.dateFormatter = new google.visualization.DateFormat({ pattern: 'yyyy/MM/dd' });
        this.dataTable = new google.visualization.DataTable();
        google.visualization.events.addListener(this.chart, 'select', () => {
            var selection = this.chart.getSelection()[0];
            if (selection != null) {
                var value = this.dataTable.getValue(selection.row, 0);
                var date = this.dateFormatter.formatValue(value)
                if (this.selectedDateChanged != null &&
                    this.selectedDateChanged.onSelectedDateChanged != null) {
                    this.selectedDateChanged.onSelectedDateChanged(date);
                }
            }
        });
    }

    public draw(): void {
        var minDate: string = this.dateFormatter.formatValue(this.minDate);
        var maxDate: string = this.dateFormatter.formatValue(this.maxDate);
        jQuery.ajax({
            type: 'GET',
            url: encodeURI(this.requestUrl + '?maxdate=' + maxDate + '&mindate=' + minDate),
            timeout: 0,
            success: (json) => {
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
                        slantedText: true,
                    },
                    vAxis: {
                        maxValue: 50,
                        minValue: 0
                    }
                };
                this.maxDate = new Date(json.maxDate);
                this.minDate = new Date(json.minDate);
                this.dataTable.addRows(json.items.length);
                this.dataTable.addColumn('date', '日付');
                this.dataTable.addColumn('number', '件数');
                jQuery.each(json.items, (index, element) => {
                    this.dataTable.setValue(index, 0, new Date(element.date));
                    this.dataTable.setValue(index, 1, element.count);
                });
                this.chart.draw(this.dataTable, option);
            },
            error: () => {
                jQuery(this.element).html('問題が発生しました。ページを再読み込みしてください。');
            }
        });
    }

}

class TweetForecast extends Chart {

    public constructor() {
        super('chart-forecast');
        var nowDate: Date = new Date();
        this.minDate = new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate());
        this.minDate.setTime(this.minDate.getTime() + (24 * 60 * 60 * 1000));
        this.maxDate = new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate());
        this.maxDate.setTime(this.maxDate.getTime() + (24 * 60 * 60 * 1000) * 31);
        this.requestUrl = '/api/forecast';
    }

}

class TweetResult extends Chart {

    public constructor() {
        super('chart-result');
        var nowDate: Date = new Date();
        this.minDate = new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate());
        this.minDate.setTime(this.minDate.getTime() - (24 * 60 * 60 * 1000) * 30);
        this.maxDate = new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate());
        this.requestUrl = '/api/result';
    }

}

class TweetLog implements ISelectedDateChanged {

    private dateFormatter: google.visualization.DateFormat;
    private selectedDate: any;
    private itemArray: any;
    private itemExists: any;

    public constructor() {
        this.dateFormatter = new google.visualization.DateFormat({ pattern: 'yyyy/MM/dd' });
        this.selectedDate = ko.observable();
        this.itemArray = ko.observableArray();
        this.itemExists = ko.computed(() => this.itemArray().length > 0);
        if (window.location.hash.length > 0) {
            var value = decodeURIComponent(window.location.hash.replace('#', ''));
            var date = this.dateFormatter.formatValue(new Date(value))
            this.retrive(date);
        }
    }

    public retrive(date: string) {
        this.selectedDate(date);
        jQuery('#tweet-log').hide();
        jQuery.ajax({
            type: 'GET',
            url: encodeURI('/api/log?date=' + date),
            timeout: 0,
            success: (json) => {
                this.itemArray.removeAll();
                ko.utils.arrayPushAll(this.itemArray, json);
                twemoji.parse(document.body);
                $('.tweet-item-text').each(function () {
                    $(this).html(
                        $(this).html().replace(
                            /((http|https|ftp):\/\/[\w?=&.\/-;#~%-]+(?![\w\s?&.\/;#~%"=-]*>))/g,
                            '<a href="$1">$1</a>'));
                });
                $('#tweet-log').fadeIn();
            },
            error: () => {
                this.itemArray.removeAll();
                $('#tweet-log').fadeIn();
            }
        });
        window.location.href = window.location.pathname + '#' + encodeURIComponent(date);
    }

    public onSelectedDateChanged(date: string): void {
        this.retrive(date);
    }

}

google.load('visualization', '1', { packages: ['corechart'] });
google.setOnLoadCallback(() => {
    var tweetLog: TweetLog = new TweetLog();
    var tweetForecast: TweetForecast = new TweetForecast();
    var tweetResult: TweetResult = new TweetResult();
    tweetResult.selectedDateChanged = tweetLog;
    ko.applyBindings({
        tweetLog: tweetLog,
        tweetForecast: tweetForecast,
        tweetResult: tweetResult,
    });
    tweetForecast.draw();
    tweetResult.draw();
});
