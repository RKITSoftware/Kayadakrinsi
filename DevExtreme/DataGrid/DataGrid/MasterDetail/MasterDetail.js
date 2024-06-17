import { states, cities } from "./Data.js";

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
                    columns: [{
                        dataField: "name",
                    }, {
                        dataField: "population"
                    }],
                }).appendTo(container);
            },
        },
        export: {
            enabled: true,
            allowExportSelectedData:true,
        },
        selection: {
            mode:"multiple",
        },
        onExporting: function (e) {
            var workbook = new ExcelJS.Workbook();
            var worksheet = workbook.addWorksheet('Population');

            let masterRows = [];

            DevExpress.excelExporter.exportDataGrid({
                component: e.component,
                worksheet: worksheet,
                topLeftCell: { row: 2, column: 2 },
                customizeCell: function ({ gridCell, excelCell }) {
                    if (gridCell.column.index === 1 && gridCell.rowType === 'data') {
                        masterRows.push({ rowIndex: excelCell.fullAddress.row + 1, data: gridCell.data });
                    }
                }
            }).then((cellRange) => {
                const borderStyle = { style: "thin", color: { argb: "FF7E7E7E" } };
                let offset = 0;

                const insertRow = (index, offset, outlineLevel) => {
                    const currentIndex = index + offset;
                    const row = worksheet.insertRow(currentIndex, [], 'n');

                    for (var j = worksheet.rowCount + 1; j > currentIndex; j--) {
                        worksheet.getRow(j).outlineLevel = worksheet.getRow(j - 1).outlineLevel;
                    }

                    row.outlineLevel = outlineLevel;

                    return row;
                }

                for (var i = 0; i < masterRows.length; i++) {
                    let row = insertRow(masterRows[i].rowIndex + i, offset++, 2);
                    let columnIndex = cellRange.from.column + 1;
                    row.height = 40;

                    let stateData = states.find((item) => item.id === masterRows[i].data.id);
                    console.log(stateData)
                    Object.assign(row.getCell(columnIndex), {
                        value: getStateCaption(stateData),
                        fill: { type: 'pattern', pattern: 'solid', fgColor: { argb: 'BEDFE6' } }
                    });
                    worksheet.mergeCells(row.number, columnIndex, row.number, 4);

                    const columns = ["name", "population"];

                    row = insertRow(masterRows[i].rowIndex + i, offset++, 2);
                    columns.forEach((columnName, currentColumnIndex) => {
                        Object.assign(row.getCell(columnIndex + currentColumnIndex), {
                            value: columnName,
                            fill: { type: 'pattern', pattern: 'solid', fgColor: { argb: 'BEDFE6' } },
                            font: { bold: true },
                            border: { bottom: borderStyle, left: borderStyle, right: borderStyle, top: borderStyle }
                        });
                    });

                    getPopulation(stateData.id).forEach((city, index) => {
                        row = insertRow(masterRows[i].rowIndex + i, offset++, 2);

                        columns.forEach((columnName, currentColumnIndex) => {
                            Object.assign(row.getCell(columnIndex + currentColumnIndex), {
                                value: city[columnName],
                                fill: { type: 'pattern', pattern: 'solid', fgColor: { argb: 'BEDFE6' } },
                                border: { bottom: borderStyle, left: borderStyle, right: borderStyle, top: borderStyle }
                            });
                        });
                    });
                    offset--;
                }
            }).then(function () {
                // https://github.com/exceljs/exceljs#writing-xlsx
                workbook.xlsx.writeBuffer().then(function (buffer) {
                    saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Population.xlsx');
                });
            });
            e.cancel = true;
        },
    }).dxDataGrid("instance");

});


const getStateCaption = ({ name }) => `Cities of ${name} : `;

const getPopulation = (id) => cities.filter((city) => city.stateid === id);