$(function() {
  $(".navbar-expand-toggle").click(function() {
    $(".app-container").toggleClass("expanded");
    return $(".navbar-expand-toggle").toggleClass("fa-rotate-90");
  });
  return $(".navbar-right-expand-toggle").click(function() {
    $(".navbar-right").toggleClass("expanded");
    return $(".navbar-right-expand-toggle").toggleClass("fa-rotate-90");
  });
});

$(function() {
  //return $('select').select2();
});

$(function() {
  /*return $('.toggle-checkbox').bootstrapSwitch({
    size: "small"
  });*/
});

$(function() {
  //return $('.match-height').matchHeight();
});

$(function() {
  /*return $('.datatable').DataTable({
    "dom": '<"top"fl<"clear">>rt<"bottom"ip<"clear">>'
  });*/
});

$(function() {
  return $(".side-menu .nav .dropdown").on('show.bs.collapse', function() {
    return $(".side-menu .nav .dropdown .collapse").collapse('hide');
  });
});
