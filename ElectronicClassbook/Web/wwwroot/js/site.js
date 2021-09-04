$(document).ready(function () {
    $('#RolesDropdown').multiselect();
});
$(document).ready(function () {
    $('#ParentsDropdown').multiselect();
});
$(document).ready(function () {
    $('#SubjectsDropdown').multiselect();
});
$(document).ready(function () {
    $('#StudentsDropdown').multiselect();
});
$(document).ready(function () {
    $('#ClassesDropdown').multiselect();
});

$(function () {
    $('[data-target="showmenu"]').on("click", function () {
        $('.sidebar-responsive').toggleClass('active')
    });
});

