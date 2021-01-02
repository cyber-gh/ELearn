const cacheImages = async (src: string[]) => {
    const promises = src.map(x => {
        return new Promise((resolve, reject) => {
            const img = new Image();
            img.src = x;
            img.onload = resolve;
            img.onerror = reject;
        })
    })
    await Promise.all(promises);
}

const generateRandomString = () => {
    let s1 = Math.random().toString(36).substring(7);
    let s2 = Math.random().toString(36).substring(7);
    let s3 = Math.random().toString(36).substring(7);
    return s1 + s2 + s3;
}

const withFallback = async (setSnackbar, action) => {
    try {
        await action()
    } catch (e) {
        console.log(e.message);
        setSnackbar({
            message: e.message,
            type: "error"
        })
    }
}

const breakpoints = {
    mobile: 480,
    tablet: 768,
    smallScreen: 1024,
    largeScreen: 1200,
}

export {cacheImages, generateRandomString, breakpoints, withFallback}