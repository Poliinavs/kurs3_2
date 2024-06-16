document.addEventListener('keydown', function (event) {
    console.log('before if: ' + event.key);
    if (event.ctrlKey && event.key === '1') {
        window.location.href = 'http://localhost:5108/Auth?shortcut=ctrl+1';
        console.log('Сочетание клавиш Ctrl+1 нажато');
        alert('Сочетание клавиш Ctrl+1 нажато'); // Вывод текста при нажатии сочетания клавиш
    }
});