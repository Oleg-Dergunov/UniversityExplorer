// Applies the saved colour theme as early as possible, before the page is painted,
// to avoid a flash of the wrong theme. Falls back to the operating system preference.
(function () {
    const storageKey = 'universityExplorer.theme';

    function getSavedTheme() {
        try {
            return localStorage.getItem(storageKey);
        } catch {
            return null;
        }
    }

    function getPreferredTheme() {
        const saved = getSavedTheme();

        if (saved === 'light' || saved === 'dark') {
            return saved;
        }

        return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
    }

    function applyTheme(theme) {
        document.documentElement.setAttribute('data-bs-theme', theme);
    }

    window.ueTheme = {
        toggle: function () {
            const current = document.documentElement.getAttribute('data-bs-theme');
            const next = current === 'dark' ? 'light' : 'dark';

            try {
                localStorage.setItem(storageKey, next);
            } catch {
            }

            applyTheme(next);
        }
    };

    applyTheme(getPreferredTheme());

    document.addEventListener('DOMContentLoaded', function () {
        if (window.Blazor) {
            Blazor.addEventListener('enhancedload', function () {
                applyTheme(getPreferredTheme());
            });
        }
    });
})();