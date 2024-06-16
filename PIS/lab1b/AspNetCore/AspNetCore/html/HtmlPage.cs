namespace AspNetCore.html
{
    public static class HtmlPage
    {
        public static string GetHtmlPage()
        {
            return @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
</head>
<body>
    <div id=""messageCount"">message amount: 0</div>
    <div id=""controls"">
        <button id=""startButton"">start</button>
        <button id=""stopButton"" disabled>stop</button>
    </div>
    <div id=""output""></div>

    <script>
        const startButton = document.getElementById('startButton');
        const stopButton = document.getElementById('stopButton');
        const outputDiv = document.getElementById('output');
        const messageCountDiv = document.getElementById('messageCount');
        let socket;
        let messageCount = 0;

        startButton.addEventListener('click', () => {
            socket = new WebSocket('ws://localhost:5236/websocket');
            socket.onmessage = (event) => {
                const message = event.data;
                outputDiv.innerHTML += `<p class=""message"">${message}</p>`;
                messageCount++;
                messageCountDiv.textContent = `message amount: ${messageCount}`;
            };
            startButton.disabled = true;
            stopButton.disabled = false;
        });

        stopButton.addEventListener('click', () => {
            if (socket) {
                socket.close();
                startButton.disabled = false;
                stopButton.disabled = true;
            }
        });
    </script>
</body>
</html>";
        }

    }
}
