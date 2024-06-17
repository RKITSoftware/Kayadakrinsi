import { states, cities, citiesAdvance } from "../MasterDetail/Data.js";

$(() => {

    const grid = $("#grid").dxDataGrid({
        dataSource: states,
        keyExpr: "id",
        masterDetail: {
            autoExpandAll: false, // Default : false
            enabled: true,
            template(container, options) {
                const currentStateData = options.data;

                $('<div>')
                    .addClass('master-detail-caption')
                    .text(`Cities of ${currentStateData.name}`)
                    .appendTo(container);

                $('<div>').dxDataGrid({
                    dataSource: new DevExpress.data.DataSource({
                        store: new DevExpress.data.ArrayStore({
                            key: 'id',
                            data: cities,
                        }),
                        filter: ['stateid', '=', options.key],
                    }),
                    pager: {
                        allowedPageSizes: [3, 6, 'all'],
                        showInfo: true,
                        showNavigationButtons: true,
                        showPageSizeSelector: true,
                        visible: true,
                    },
                    paging: {
                        pageSize: 3,
                    },
                    columns: [{
                        dataField: "name",
                    }],
                    masterDetail: {
                        autoExpandAll: false, // Default : false
                        enabled: true,
                        template(container, options) {
                            const currentCityData = options.data;

                            $('<div>')
                                .addClass('master-detail-caption')
                                .text(`Population of ${currentCityData.name}`)
                                .appendTo(container);

                            $('<div>').dxDataGrid({
                                dataSource: new DevExpress.data.DataSource({
                                    store: new DevExpress.data.ArrayStore({
                                        key: 'id',
                                        data: citiesAdvance,
                                    }),
                                    filter: ['id', '=', options.key],
                                }),
                                columns: [{
                                    caption: "1991-2001 Population",
                                    cellTemplate: function (container, options) {
                                        $("<div>")
                                            .text(options.data.population["1991-2001"])
                                            .appendTo(container);
                                    }
                                },
                                {
                                    caption: "2001-2011 Population",
                                    cellTemplate: function (container, options) {
                                        $("<div>")
                                            .text(options.data.population["2001-2011"])
                                            .appendTo(container);
                                    }
                                },
                                {
                                    caption: "2011-2021 Population",
                                    cellTemplate: function (container, options) {
                                        $("<div>")
                                            .text(options.data.population["2011-2021"])
                                            .appendTo(container);
                                    }
                                },
                                {
                                    caption: "Current Population",
                                    cellTemplate: function (container, options) {
                                        $("<div>")
                                            .text(options.data.population["2021"])
                                            .appendTo(container);
                                    }
                                    },
                                    {
                                        caption: "Statistics",
                                        cellTemplate: function (container, options) {
                                            var chartData = [];
                                            $.each(options.data.population, function (year, population) {
                                                chartData.push({ year: year, population: population });
                                            });

                                            $("<div>")
                                                .dxChart({
                                                    dataSource: chartData,
                                                    series: {
                                                        argumentField: "year",
                                                        valueField: "population",
                                                        type: "line"
                                                    },
                                                    tooltip: {
                                                        enabled: true
                                                    },
                                                    size: {
                                                        width: 150,
                                                        height: 100
                                                    }
                                                }).appendTo(container);
                                        }
                                    }],
                            }).appendTo(container);
                        },
                    },
                }).appendTo(container);
            },
        },
    }).dxDataGrid("instance");

});