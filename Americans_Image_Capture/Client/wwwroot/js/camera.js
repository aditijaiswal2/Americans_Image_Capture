

async function startVideo(src) {
    try {
        const permission = await checkCameraPermission();
        if (permission === 'prompt' || permission === 'granted') {
            navigator.getUserMedia(
                { video: true, audio: false },
                function (localMediaStream) {
                    let video = document.getElementById(src);
                    video.srcObject = localMediaStream;
                    video.onloadedmetadata = function (e) {
                        video.play();
                    };
                },
                function (err) {
                    console.error('Error accessing camera:', err);
                    throw err; // Propagate the error
                }
            );
        } else {
            console.error('Camera permission denied.');
        }
    } catch (error) {
        console.error('Error starting video:', error);
    }
}



function getFrame(src, dest, dotnetHelper) {
    let video = document.getElementById(src);
    let canvas = document.getElementById(dest);

    // Check if the video and canvas elements exist before drawing the image
    if (video && canvas) {
        canvas.getContext('2d').drawImage(video, 0, 0, 150, 150);

        // Resize the image on the canvas
        let resizedCanvas = document.createElement('canvas');
        resizedCanvas.width = 200; // Set desired width
        resizedCanvas.height = 200; // Set desired height
        let ctx = resizedCanvas.getContext('2d');
        ctx.drawImage(canvas, 0, 0, resizedCanvas.width, resizedCanvas.height);

        // Convert the resized image to base64 JPEG format
        let dataUrl = resizedCanvas.toDataURL("image/jpeg");

        // Invoke the .NET method with the resized image data
        dotnetHelper.invokeMethodAsync('ProcessImage', dataUrl);
    } else {
        console.error('Video or canvas element not found.');
    }
}


function stopVideo(src) {
    let video = document.getElementById(src);

    // Check if the video element exists before stopping
    if (video) {
        if ('srcObject' in video) {
            let tracks = video.srcObject.getTracks();
            tracks.forEach(track => track.stop());
            video.srcObject = null;
        } else {
            video.src = '';
        }
    } else {
        console.error('Video element with ID ' + src + ' not found.');
    }
}

function closeCamera() {
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        navigator.mediaDevices.getUserMedia({ video: true })
            .then(function (stream) {
                // Stop all tracks
                let tracks = stream.getTracks();
                tracks.forEach(track => track.stop());
            })
            .catch(function (error) {
                console.error('Error closing the camera: ', error);
            });
    } else {
        console.error('getUserMedia is not supported on this browser.');
    }
}

//async function selectFolderAndSaveFiles() {
//    try {
//        const dirHandle = await window.showDirectoryPicker();
//        const files = []; // Add files to save here

//        for (const file of files) {
//            const fileHandle = await dirHandle.getFileHandle(file.name, { create: true });
//            const writable = await fileHandle.createWritable();
//            await writable.write(file.content);
//            await writable.close();
//        }
//        return "Files saved successfully.";
//    } catch (err) {
//        console.error("Error selecting folder:", err);
//        return "Failed to save files.";
//    }
//}
async function selectFolderAndSaveFiles(filesToSave) {
    try {
        // Open the folder picker dialog
        const dirHandle = await window.showDirectoryPicker();
        const dirPath = dirHandle.name;

        let savedFilePaths = [];

        // Iterate over the selected files and save them into the selected directory
        for (const file of filesToSave) {
            // Create a new file handle in the directory
            const fileHandle = await dirHandle.getFileHandle(file.name, { create: true });

            // Create a writable stream for the file
            const writable = await fileHandle.createWritable();

            // Convert the base64 data back to a Blob
            const byteCharacters = atob(file.base64Data); // Decode base64 string into binary data
            const byteArrays = new Uint8Array(byteCharacters.length);

            for (let i = 0; i < byteCharacters.length; i++) {
                byteArrays[i] = byteCharacters.charCodeAt(i);
            }

            // Write the byte array to the file
            await writable.write(byteArrays);
            await writable.close(); // Close the writable stream to finalize saving

            // Add the file path to the array (assuming you're saving in the folder)
            savedFilePaths.push(`${dirPath}/${file.name}`);
        }

        // Return the paths of the saved files
        return savedFilePaths;

    } catch (err) {
        console.error("Error saving files:", err);
        return "Failed to save files.";
    }
}
