console.log("Hello");

function MoveText() {
    var text1 = document.getElementById("textbox1");
    var text2 = document.getElementById("textbox2");

    console.log(text1.value);
    console.log(text2.value);

    text2.value = text1.value;
    text1.value = "";

}



function moveOption(direction) {

    //var a = 1;
    //var b = "1";

    //a == b // true
    //a === b // false because it checks the types

    var leftSelectElement = document.getElementById("leftSelect");
    var rightSelectElement = document.getElementById("rightSelect");

    if (direction === 'left')
    {
        moveFromTo(rightSelectElement, leftSelectElement);
    }
    else {
        moveFromTo(leftSelectElement, rightSelectElement);

    }
}


function moveFromTo(elementFrom, ElementTo) {

    var optionsToMove = [];

    var selectedOptions = elementFrom.selectedOptions;
    console.log(selectedOptions);
    if (selectedOptions.length > 0) {
        for (let i = selectedOptions.length - 1; i >= 0; i--) {
            let element = selectedOptions[i];
            optionsToMove.push(element);
        }
        console.log(optionsToMove);
        for (let i = optionsToMove.length - 1; i >= 0; i--) {
            let element = optionsToMove[i];
            ElementTo.appendChild(element);
        }
    }


}


function moveFromToold(elementFrom, ElementTo) {
    selectedOptions = elementFrom.selectedOptions;
    console.log(selectedOptions);
    if (selectedOptions.length > 0) {
        for (let i = selectedOptions.length - 1; i >= 0; i--) {
            let element = selectedOptions[i];
            ElementTo.appendChild(element);
        }
    }
}

