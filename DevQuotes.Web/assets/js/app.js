'use strict';

window.onload = () => {
    getQuote();
    setInterval(getQuote, 10000);
}

var text = '';
var speed = 50;
var counter = 0;

hljs.configure({ ignoreUnescapedHTML: true });

var codePlace = document.querySelector("#code-place");

function typeWriter() {
  if (counter < text.length) {
    codePlace.innerHTML += new Option(text.charAt(counter)).innerHTML;

    counter++;

    setTimeout(typeWriter, speed);
    hljs.highlightAll();
  }
}

function deleteCode() {
    text = '';
    counter = 0;
    codePlace.innerHTML = '';
}

async function getQuote() {
    await fetch('https://codequotes.herokuapp.com/api/v1/quote/random').then((response) => {
        response.json().then(async (data) => {
            deleteCode()
            text = data.quote.trim();
            typeWriter();
        });
    })
    .catch((error) => console.error(error));
}