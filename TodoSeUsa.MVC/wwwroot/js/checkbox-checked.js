function validate(id, elem, child) {
    if (document.getElementById(id).checked) {
        show(elem)
    } else {
/*        hide(elem)*/
        reset(child)
    }
}

/*function hide(elem) {
    document.getElementById(elem).hidden = true
}*/
function show(elem) {
    document.getElementById(elem).hidden = false
}

function reset(input) {
    var element = document.getElementById(input)
    if (element != null)
    {
        if (element.id == "devuelto-switch") {

            document.getElementById(input).checked = false
        }
        else if (element.id == "reacondicionado-input")
        {
            document.getElementById("reacondicionado-input").value = 0
        }
    }
}
