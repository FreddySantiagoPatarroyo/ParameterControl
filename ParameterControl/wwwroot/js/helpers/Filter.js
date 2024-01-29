let viewOptionsFilter = false;

const showOptions = () => {

    let widthOption = document.getElementById("input_select").clientWidth;
    let options = document.getElementById("options");
    options.style.width = widthOption + "px";

    viewOptionsFilter = !viewOptionsFilter;

    if (viewOptionsFilter) {
        document.getElementById('options').style.display = 'block';
        document.getElementById('select_helper').style.display = 'block';
    } else {
        document.getElementById('options').style.display = 'none';
        document.getElementById('select_helper').style.display = 'none';
    }
}

let viewOptionsFilterSecondary = false;

const showSecondaryOptions = () => {

    let widthOption = document.getElementById("input_select_options").clientWidth;
    let options = document.getElementById("secondary_options");
    options.style.width = widthOption + "px";

    viewOptionsFilterSecondary = !viewOptionsFilterSecondary;

    if (viewOptionsFilterSecondary) {
        document.getElementById('secondary_options').style.display = 'block';
        document.getElementById('select_helper_secondary').style.display = 'block';
    } else {
        document.getElementById('secondary_options').style.display = 'none';
        document.getElementById('select_helper_secondary').style.display = 'none';
    }
}

const selectOption = async(name, value) => {

    if (name == null || value == null || name == '' || value == '') {
        document.getElementById("input_select_filter").value = '';
        document.getElementById("input_select_filter_value").value = '';
        document.getElementById("input_filter").value = '';
        document.getElementById("input_filter_date").value = '';
        document.getElementById("input_select_filter_options").value = '';
        document.getElementById("input_select_filter_options_value").value = '';
        document.getElementById('field_general').style.display = 'none';
        document.getElementById('field_date').style.display = 'none';
        document.getElementById('field_options').style.display = 'none';
    } else {
        const json = await selectSecondInputFilter(value);

        document.getElementById("TypeRow").value = json.typeRow;

        if (json.typeRow == "General") {
            document.getElementById("input_select_filter").value = name;
            document.getElementById("input_select_filter_value").value = value;
            document.getElementById("input_filter").value = '';
            document.getElementById("input_filter_date").value = '';
            document.getElementById("input_select_filter_options").value = '';
            document.getElementById("input_select_filter_options_value").value = '';
            document.getElementById('field_general').style.display = 'block';
            document.getElementById('field_date').style.display = 'none';
            document.getElementById('field_options').style.display = 'none';
        } else if (json.typeRow == "Date") {
            document.getElementById("input_select_filter").value = name;
            document.getElementById("input_select_filter_value").value = value;
            document.getElementById("input_filter").value = '';
            document.getElementById("input_filter_date").value = new Date();
            document.getElementById("input_select_filter_options").value = '';
            document.getElementById("input_select_filter_options_value").value = '';
            document.getElementById('field_general').style.display = 'none';
            document.getElementById('field_date').style.display = 'block';
            document.getElementById('field_options').style.display = 'none';
        }
        else if (json.typeRow == "Select") {
            document.getElementById("input_select_filter").value = name;
            document.getElementById("input_select_filter_value").value = value;
            document.getElementById("input_filter").value = '';
            document.getElementById("input_filter_date").value = '';
            document.getElementById("input_select_filter_options").value = '';
            document.getElementById("input_select_filter_options_value").value = '';
            document.getElementById('field_general').style.display = 'none';
            document.getElementById('field_date').style.display = 'none';
            document.getElementById('field_options').style.display = 'block';

            const options = json.options.map(option => `<li onclick="selectSecondaryOption('${option.text}', '${option.text}')"  class="option"> <p class="option_text">${option.text}</p> </li>`);

            options.unshift(`<li onclick="selectSecondaryOption(${null}, ${null})"  class="option"> <p class="option_text">${'Seleccione una opción'}</p> </li>`);

            $('#secondary_options_list').html(options);
        }
    }

    showOptions();
}

const selectSecondaryOption = (name, value) => {

    if (name == null || value == null || name == '' || value == '') {
        document.getElementById("input_select_filter_options").value = '';
        document.getElementById("input_select_filter_options_value").value = '';
    } else {
        document.getElementById("input_select_filter_options").value = name;
        document.getElementById("input_select_filter_options_value").value = value;
    }
    showSecondaryOptions();
}

const selectSecondInputFilter = async(value) => {
    const url = 'GetSecondaryFilter';
    const data = value;

    const result = await fetch(url, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: {
            'Content-Type': 'application/json'
        }
    })

    const json = await result.json();
    return json;
}