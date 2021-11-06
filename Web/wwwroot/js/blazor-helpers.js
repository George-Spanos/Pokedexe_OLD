window.scrollToBottom = function () {
    window.scrollTo(0, document.body.scrollHeight);
}
window.focusInput = function (id) {
    document.getElementById(id).focus();
}
window.notify = function () {
    document.getElementById('notification').play()
}