const insertedHandler = function (values, key) {
   console.log(`Inserted : ${values}, ${key}`);
};

const insertingHandler = function (values) {
   console.log(`Inserting : ${values}`);
};

const loadedHandler = function (result) {
   console.log(`Loaded : ${result}`);
};

const loadingHandler = function (loadOptions) {
   console.log(`Loading : ${loadOptions}`);
};

const modifiedHandler = function () {
   console.log(`Modified`);
};

const modifyingHandler = function () {
   console.log(`Modifying`);
};

const pushHandler = function (changes) {
   console.log(`Push : ${changes}`);
};

const removedHandler = function (key) {
   console.log(`Removed : ${key}`);
};

const removingHandler = function (key) {
   console.log(`Removing : ${key}`);
};

const updatedHandler = function (key, values) {
   console.log(`Updated : ${values}, ${key}`);
};

const updatingHandler = function (key, values) {
   console.log(`Updating : ${values}, ${key}`);
};

function rowClickHandler(e) {
    const eventDetails = [
        { name: 'Columns', value: e.columns },
        { name: 'Component', value: e.component },
        { name: 'Row data', value: e.data },
        { name: 'Element', value: e.element },
        { name: 'Event', value: e.event },
        { name: 'Group Index', value: e.groupIndex },
        { name: 'Handled', value: e.handled },
        { name: 'Is Expanded', value: e.isExpanded },
        { name: 'Is New Row', value: e.isNewRow },
        { name: 'Is Selected', value: e.isSelected },
        { name: 'Row Key', value: e.key },
        { name: 'Model', value: e.model },
        { name: 'Row Element', value: e.rowElement },
        { name: 'Row Index', value: e.rowIndex },
        { name: 'Row Type', value: e.rowType },
        { name: 'Values', value: e.values }
    ];

    console.log('Row click event details:', eventDetails);
}


function rowCollapsedHandler(e) {
    const eventDetails = [
        { name: 'Component', value: e.component },
        { name: 'Element', value: e.element },
        { name: 'Key', value: e.key },
        { name: 'Model', value: e.model }
    ];

    console.log('Row collapsed event details:', eventDetails);
}


function rowCollapsingHandler(e) {
    const eventDetails = [
        { name: 'Cancel', value: e.cancel },
        { name: 'Component', value: e.component },
        { name: 'Element', value: e.element },
        { name: 'Key', value: e.key },
        { name: 'Model', value: e.model }
    ];

    console.log('Row collapsing event details:', eventDetails);
}


function rowDblClickHandler(e) {
    const eventDetails = [
        { name: 'Columns', value: e.columns },
        { name: 'Component', value: e.component },
        { name: 'Data', value: e.data },
        { name: 'Element', value: e.element },
        { name: 'Event', value: e.event },
        { name: 'Group Index', value: e.groupIndex },
        { name: 'Is Expanded', value: e.isExpanded },
        { name: 'Is New Row', value: e.isNewRow },
        { name: 'Is Selected', value: e.isSelected },
        { name: 'Key', value: e.key },
        { name: 'Model', value: e.model },
        { name: 'Row Element', value: e.rowElement },
        { name: 'Row Index', value: e.rowIndex },
        { name: 'Row Type', value: e.rowType },
        { name: 'Values', value: e.values }
    ];

    console.log('Row double clicked event details:', eventDetails);
}


function rowExpandedHandler(e) {
    const eventDetails = [
        { name: 'Component', value: e.component },
        { name: 'Element', value: e.element },
        { name: 'Key', value: e.key },
        { name: 'Model', value: e.model }
    ];

    console.log('Row expanded event details:', eventDetails);
}


function rowExpandingHandler(e) {
    const eventDetails = [
        { name: 'Cancel', value: e.cancel },
        { name: 'Component', value: e.component },
        { name: 'Element', value: e.element },
        { name: 'Key', value: e.key },
        { name: 'Model', value: e.model }
    ];

    console.log('Row expanding event details:', eventDetails);
}


function rowInsertedHandler(e) {
    const eventDetails = [
        { name: 'Component', value: e.component },
        { name: 'Data', value: e.data },
        { name: 'Element', value: e.element },
        { name: 'Error', value: e.error },
        { name: 'Key', value: e.key },
        { name: 'Model', value: e.model }
    ];

    console.log('Row inserted event details:', eventDetails);
}


function rowInsertingHandler(e) {
    const eventDetails = [
        { name: 'Cancel', value: e.cancel },
        { name: 'Component', value: e.component },
        { name: 'Data', value: e.data },
        { name: 'Element', value: e.element },
        { name: 'Model', value: e.model }
    ];

    console.log('Row inserting event details:', eventDetails);
}
 

function rowPreparedHandler(e) {
    const eventDetails = [
        { name: 'Columns', value: e.columns },
        { name: 'Component', value: e.component },
        { name: 'Data', value: e.data },
        { name: 'Element', value: e.element },
        { name: 'Group Index', value: e.groupIndex },
        { name: 'Is Expanded', value: e.isExpanded },
        { name: 'Is New Row', value: e.isNewRow },
        { name: 'Is Selected', value: e.isSelected },
        { name: 'Key', value: e.key },
        { name: 'Model', value: e.model },
        { name: 'Row Element', value: e.rowElement },
        { name: 'Row Index', value: e.rowIndex },
        { name: 'Row Type', value: e.rowType },
        { name: 'Values', value: e.values }
    ];

    console.log('Row prepared event details:', eventDetails);
}


function rowRemovedHandler(e) {
    const eventDetails = [
        { name: 'Component', value: e.component },
        { name: 'Data', value: e.data },
        { name: 'Element', value: e.element },
        { name: 'Error', value: e.error },
        { name: 'Key', value: e.key },
        { name: 'Model', value: e.model }
    ];

    console.log('Row removed event details:', eventDetails);
}


function rowRemovingHandler(e) {
    const eventDetails = [
        { name: 'Cancel', value: e.cancel },
        { name: 'Component', value: e.component },
        { name: 'Data', value: e.data },
        { name: 'Element', value: e.element },
        { name: 'Key', value: e.key },
        { name: 'Model', value: e.model }
    ];

    console.log('Row removing event details:', eventDetails);
}


function rowUpdatedHandler(e) {
    const eventDetails = [
        { name: 'Component', value: e.component },
        { name: 'Data', value: e.data },
        { name: 'Element', value: e.element },
        { name: 'Error', value: e.error },
        { name: 'Key', value: e.key },
        { name: 'Model', value: e.model }
    ];

    console.log('Row updated event details:', eventDetails);
}


function rowUpdatingHandler(e) {
    const eventDetails = [
        { name: 'Cancel', value: e.cancel },
        { name: 'Component', value: e.component },
        { name: 'Element', value: e.element },
        { name: 'Key', value: e.key },
        { name: 'Model', value: e.model },
        { name: 'New Data', value: e.newData },
        { name: 'Old Data', value: e.oldData }
    ];

    console.log('Row updating event details:', eventDetails);
}


function rowValidatingHandler(e) {
    const eventDetails = [
        { name: 'Broken Rules', value: e.brokenRules },
        { name: 'Component', value: e.component },
        { name: 'Element', value: e.element },
        { name: 'Error Text', value: e.errorText },
        { name: 'Is Valid', value: e.isValid },
        { name: 'Key', value: e.key },
        { name: 'Model', value: e.model },
        { name: 'New Data', value: e.newData },
        { name: 'Old Data', value: e.oldData },
        { name: 'Promise', value: e.promise }
    ];

    console.log('Row validating event details:', eventDetails);
}


export { insertedHandler, insertingHandler, loadedHandler, loadingHandler, modifiedHandler, modifyingHandler, pushHandler, removedHandler, removingHandler, updatedHandler, updatingHandler, rowClickHandler, rowCollapsedHandler, rowCollapsingHandler, rowDblClickHandler, rowExpandedHandler, rowExpandingHandler, rowInsertedHandler, rowInsertingHandler, rowPreparedHandler, rowRemovedHandler,rowRemovingHandler,rowUpdatedHandler,rowUpdatingHandler,rowValidatingHandler };