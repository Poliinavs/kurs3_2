namespace lab1a
{
    public static class HtmlPage
    {
        public static string GetHtmlPage()
        {
            return @"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Multiply Page</title>
                <script>
                    async function multiply() {
                        const x = document.getElementById('x').value;
                        const y = document.getElementById('y').value;

                        const response = await fetch('/multiply', {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/x-www-form-urlencoded',
                            },
                            body: `x=${x}&y=${y}`
                        });

                        const result = await response.text();
                        document.getElementById('result').innerText = `Result: ${result}`;
                    }
                </script>
            </head>
            <body>
                <h1>Multiply Page</h1>
                <form>
                    <label for='x'>Enter X:</label>
                    <input type='text' id='x' name='x'><br>

                    <label for='y'>Enter Y:</label>
                    <input type='text' id='y' name='y'><br>

                    <input type='button' value='Multiply' onclick='multiply()'>
                </form>

                <div id='result'></div>
            </body>
            </html>";
        }

        public static string GetHtmlPage1()
        {
            return @"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Multiply Page</title>
               <script>
    async function multiply() {
        const x = document.getElementById('x').value;
        const y = document.getElementById('y').value;

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/multiply', true);
        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        xhr.onreadystatechange = function () {
            if (xhr.readyState !== XMLHttpRequest.DONE) return;

            if (xhr.status === 200) {
                alert('Result: ' + xhr.responseText);
            } else {
                alert('Error: ' + xhr.status);
            }
        };

        xhr.send('x=' + encodeURIComponent(x) + '&y=' + encodeURIComponent(y));
    }
</script>

            </head>
            <body>
                <h1>Multiply Page</h1>
                <form>
                    <label for='x'>Enter X:</label>
                    <input type='text' id='x' name='x'><br>

                    <label for='y'>Enter Y:</label>
                    <input type='text' id='y' name='y'><br>

                    <input type='button' value='Multiply' onclick='multiply()'>
                </form>

                <div id='result'></div>
            </body>
            </html>";
        }
    }
}
