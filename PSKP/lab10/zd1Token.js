import { createClient } from 'webdav';

const client = createClient(
    "https://webdav.yandex.ru/",
    {
        
    }
);

// Get directory contents
const directoryItems = await client.getDirectoryContents("/");
