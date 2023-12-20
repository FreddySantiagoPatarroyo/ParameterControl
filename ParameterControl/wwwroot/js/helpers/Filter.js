let viewOptionsFilter = false;

const UrlFilter = 'FilterTable';

let filterInfo = {
    Filter: {
        Name: "",
        Value: ""
    },
    ValueFilter: ""
}

const showOptions = () => {

    viewOptionsFilter = !viewOptionsFilter;

    if (viewOptionsFilter) {
        document.getElementById('options').style.display = 'block';
        document.getElementById('select_helper').style.display = 'block';
    } else {
        document.getElementById('options').style.display = 'none';
        document.getElementById('select_helper').style.display = 'none';
    }
}

const selectOption = (name, value) => {

    if (name == null || value == null || name == '' || value == '') {
        console.log("vacio");
        document.getElementById("input_select_filter").value = '';
        document.getElementById("input_filter").value = '';
        document.getElementById('field_secundary').style.display = 'none';
        console.log(UrlFilter);
        filterInfo.Filter.Name = "";
        filterInfo.Filter.Value = "";
        filterInfo.ValueFilter = document.getElementById("input_filter").value;
        console.log(filterInfo);
        sendFilterBack(filterInfo);
    } else {
        console.log(value);
        document.getElementById("input_select_filter").value = name;
        document.getElementById("input_filter").value = '';
        document.getElementById('field_secundary').style.display = 'block';
        filterInfo.Filter.Name = name;
        filterInfo.Filter.Value = value;
        console.log(filterInfo);
    }

    showOptions();
}

const selectValue = () => {
    const value = document.getElementById("input_filter").value;
    if (value == null || value == "") {
        console.log("vacio");
        filterInfo.ValueFilter = "";
        console.log(filterInfo);
        sendFilterBack(filterInfo);
    } else {
        console.log(value);
        filterInfo.ValueFilter = document.getElementById("input_filter").value;
        console.log(filterInfo);
        sendFilterBack(filterInfo);
    }
}

const sendFilterBack = async(filter) => {
    var data = JSON.stringify(filter);

    await fetch(UrlFilter, {
        method: 'POST',
        body: data,
        headers: {
            "Content-Type": "application/json"
        }
    })
}