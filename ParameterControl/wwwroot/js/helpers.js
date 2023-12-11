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