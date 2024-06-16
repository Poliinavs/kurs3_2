import { createClient } from 'webdav';

const client = createClient(
    "https://webdav.yandex.ru/",
    {
        username: "paulinaavsyukevitch",
        password: "qwerasdf1234e"
    }
);

// Get directory contents
const directoryItems = await client.getDirectoryContents("/");