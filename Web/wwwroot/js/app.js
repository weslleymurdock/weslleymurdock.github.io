window.app = {
    scrollToTop: () => {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    },

    scrollToBottom: () => {
        window.scrollTo({
            top: document.documentElement.scrollHeight,
            behavior: 'smooth'
        });
    },

    isAtTop: () => {
        return (window.scrollY || document.documentElement.scrollTop) < 50;
    },

    initGlass: () => {
        document.addEventListener("mousemove", e => {
            const x = (e.clientX / window.innerWidth) * 100;
            const y = (e.clientY / window.innerHeight) * 100;

            document.documentElement.style.setProperty('--mouse-x', x + '%');
            document.documentElement.style.setProperty('--mouse-y', y + '%');
        });
    }
};