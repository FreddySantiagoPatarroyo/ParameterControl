let viewOptionsFilter = false;

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
        document.getElementById('field_secundary').style.display = 'none';
    } else {
        console.log(value);
        document.getElementById("input_select_filter").value = name;
        document.getElementById('field_secundary').style.display = 'block';
    }

    showOptions();
}