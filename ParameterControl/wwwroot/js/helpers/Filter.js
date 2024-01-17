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

const selectOption = (name, value) => {

    if (name == null || value == null || name == '' || value == '') {
        document.getElementById("input_select_filter").value = '';
        document.getElementById("input_select_filter_value").value = '';
        document.getElementById("input_filter").value = '';
        document.getElementById('field_secundary').style.display = 'none';
    } else {
        document.getElementById("input_select_filter").value = name;
        document.getElementById("input_select_filter_value").value = value;
        document.getElementById("input_filter").value = '';
        document.getElementById('field_secundary').style.display = 'block';
    }

    showOptions();
}