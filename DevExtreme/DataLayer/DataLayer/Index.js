import { users } from "./Data.js";

$(() => {

    var storeArray = new DevExpress.data.ArrayStore({
        data: users,
        errorHandler: function (error) {
            console.log(error, error.message);
        },
        key: "id",
        onInserting: function (values) {
            console.log(values);
            //  alert(`${values} will be inserted`);
        },
        onInserted: function (values, key) {
            console.log(`${values} and ${key} inserted successfully`);
            //storeArray.load();
        },
    });

    function loadFilteredData(filterValue) {
        storeArray.load({
            filter: ["age", "contains", filterValue],
            sort: [{ selector: "name", desc: false }],
            skip: 1,
            skip: 0,
            take: 10
        }).done(function (data) {
            console.log("Filtered Data:", data);
        }).fail(function (error) {
            console.log("Load Error:", error);
        });
    }


    const arrayStore = $("#arrayStore").dxSelectBox({
        dataSource: storeArray,
        displayExpr: "name",
        acceptCustomValue: true,
        valueExpr: "id",
        onCustomItemCreating: function (data) {
            //customItemHandler(data);
            data.customItem = null;
        },
        buttons: [{
            name: "add",
            location: "after",
            options: {
                icon: "add",
                onClick: function (e) {
                    var value = arrayStore.option("text");
                    var id = users[users.length - 1].id;

                    //storeArray.push([{ type: "insert", data: { id: ++id, name: value } }]);

                    storeArray.insert({ id: ++id, name: value });
                    console.log(e);

                    //storeArray.load()
                    //    .done(function (data) {
                    //        console.log("Data", data);
                    //    })
                    //    .fail(function (error) {
                    //        console.log("Error", error);
                    //    });
                }
            }
        }, {
            name: "filter",
            location: "after",
            options: {
                icon: "dragvertical",
                onClick: function (e) {
                    var filterValue = 22;
                    loadFilteredData(filterValue);
                    storeArray.totalCount().done(function (count) {
                        console.log(count);
                    }).fail(function (error) {
                        console.log("Total Count Error:", error);
                    });
                    setTimeout(() => {
                        storeArray.clear();
                        storeArray.totalCount().done(function (count) {
                            console.log(count);
                        }).fail(function (error) {
                            console.log("Total Count Error:", error);
                        });
                    }, 5000);
                }
            }
        }, 'dropDown'],
        onValueChanged: function (e) {
            storeArray.byKey(1)
                .done(function (dataItem) {
                    console.log(dataItem);
                })
                .fail(function (error) {
                    console.log(error);
                });
        }
    }).dxSelectBox("instance");


    const storeCustom = new DevExpress.data.CustomStore({
        loadMode: "raw",
        key: "id",
        load: async function (loadOptions) {
            var x = await $.ajax({
                url: "https://jsonplaceholder.typicode.com/users",
                method: "GET",
                dataType: "json",
                data: loadOptions,
            });
            console.log("LoadOptions ",loadOptions);
            return x;
        },
        byKey: function (key) {
            var d = new $.Deferred();
            $.get("https://jsonplaceholder.typicode.com/users/" + key)
                .done(function (dataItem) {
                    console.log("DataItem",dataItem);
                    d.resolve(dataItem);
                });
            return d.promise();
        },
        cacheRawData: true,
        errorHandler: function (error) {
            console.log(error.message);
        },
        insert: function (values) {
            return $.ajax({
                url: "https://jsonplaceholder.typicode.com/users",
                method: "POST",
                data: values
            })
        },
        remove: function (key) {
            return $.ajax({
                url: "https://jsonplaceholder.typicode.com/users" + encodeURIComponent(key),
                method: "DELETE",
            })
        },
        update: function (key, values) {
            return $.ajax({
                url: "https://jsonplaceholder.typicode.com/users" + encodeURIComponent(key),
                method: "PUT",
                data: values
            })
        }
    });

    
    const customStore = $("#customStore").dxSelectBox({
        dataSource: storeCustom,
        displayExpr: "name",
        acceptCustomValue: true,
        valueExpr: "id",
        buttons: [{
            name: "filter",
            location: "after",
            options: {
                icon: "dragvertical",
                onClick: function (e) {
                    storeCustom.totalCount({
                        filter: ["name", "startswith", "C"],
                        //group:"city",
                    }).done(function (count) {
                        console.log(count);
                    }).fail(function (error) {
                        console.log("Total Count Error:", error);
                    });
                }
            }
        }, 'dropDown'],
        onValueChanged: function (e) {
            const data = customStore.getDataSource();
            storeCustom.byKey(10)
                .done(function (dataItem) {
                    console.log(dataItem);
                })
                .fail(function (error) {
                    console.log(error);
                });
        }
    }).dxSelectBox("instance");


    const storeData = new DevExpress.data.DataSource({
        //expand: "address",
        //filter: [["name", "startswith", "C"], "or", ["name", "startswith", "L"]],
        //group: "city",
        //map: function (dataItem) {
        //    return {
        //        custom: dataItem.name + " " + dataItem.id
        //    }
        //},
        //postProcess: function (data) {
        //    console.log("Post process", data);
        //},
        store: users,
        onChanged: function (e) {
            console.log("On change", e);
        },
        onLoadError: function (error) {
            console.log("On load error",error.message);
        },
        onLoadingChanged: function (isLoading) {
            console.log("On loading changed", isLoading);
        },
        paginate: true,
        pageSize: 3,
        requireTotalCount: true,
        reshapeOnPush: true,
        searchExpr: ["name"],
        searchOperation: "startswith",
        sort: { selector: "name", desc: true },

    });


    const dataSource = $("#dataSource").dxSelectBox({
        dataSource: storeData,
        displayExpr: "name",
        acceptCustomValue: true,
        valueExpr: "id",
        buttons: [{
            name: "filter",
            location: "after",
            options: {
                icon: "dragvertical",
                onClick: function (e) {
                    var count = storeData.totalCount();
                    console.log(count);
                }
            }
        }, 'dropDown'],
        onValueChanged: function (e) {
            dataSource.byKey(10)
                .done(function (dataItem) {
                    console.log(dataItem);
                })
                .fail(function (error) {
                    console.log(error);
                });
        }
    }).dxSelectBox("instance");


    const storeLocal = new DevExpress.data.LocalStore({
        key: "id",
        data: users,
        name: "myLocalData",
        immediate: false,
        flushInterval: 3000,
        errorHandler: function (error) {
            console.log("Local store error handler says : ",error.message);
        }
    });


    const localStore = $("#localStore").dxSelectBox({
        dataSource: storeLocal,
        displayExpr: "name",
        acceptCustomValue: true,
        valueExpr: "id",
        buttons: [{
            name: "filter",
            location: "after",
            options: {
                icon: "dragvertical",
                onClick: function (e) {
                    var query = storeLocal.createQuery();
                    var startsWithA = query.filter(["name", "startswith", "A"]).toArray();
                    console.log(query, startsWithA);
                }
            }
        }, 'dropDown'],
        onValueChanged: function (e) {
            storeLocal.byKey(5)
                .done(function (dataItem) {
                    console.log(dataItem);
                })
                .fail(function (error) {
                    console.log(error);
                });
        }
    }).dxSelectBox("instance");


    const queryStore = DevExpress.data.query(users)
        .filter(["name", "startswith", "c"])
        .sortBy("name")
        .select("name", "id")
        .toArray();

    setTimeout(() => {
        console.log(queryStore);
    }, 2000);
});