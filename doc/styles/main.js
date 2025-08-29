function toggleSidenav() {
  var sideNav = document.querySelector('#sidenav');
  var hidden = sidenav.classList.contains("sidenav-hidden");

  if (hidden) {
    sidenav.classList.remove("sidenav-hidden");
  } else {
    sidenav.classList.add("sidenav-hidden");
  }
}

// Default to hidden
var sidenav = document.querySelector('#sidenav')
sidenav.classList.add("sidenav-hidden");
sidenav.style.transition = "all 0s";
setTimeout(function() {
  sidenav.style.removeProperty("transition");
}, 10)

//document.querySelector('#sidetoggle').dataset.test
