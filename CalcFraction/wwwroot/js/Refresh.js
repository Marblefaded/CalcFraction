/*function clicked(percent) {
    document.getElementById('files').addEventListener("click", updating(percent));
}*/

function updating(percent) {
    document.getElementById('progress').innerHTML = percent + "%";
    
}

function clickInputFile() {
    document.getElementById("loaderImage").click();
}
