// Chart.js interop — adjusted palette and compact defaults to match app theme
window.chartInterop = (function () {
    const charts = {};
    const themeColors = [
        '#6f8499', // primary muted indigo
        '#27d6d6', // teal accent
        '#9fb1c6', // light slate
        '#f1c40f', // accent yellow
        '#e67e22', // orange
        '#e74c3c', // red
        '#2ecc71', // green
        '#9b59b6', // purple
        '#3498db', // blue
        '#95a5a6'  // gray
    ];

    function destroyIfExists(id) {
        if (charts[id]) {
            try { charts[id].destroy(); } catch { /* ignore */ }
            delete charts[id];
        }
    }

    function pickColors(count) {
        const colors = [];
        for (let i = 0; i < count; i++) colors.push(themeColors[i % themeColors.length]);
        return colors;
    }

    return {
        renderPie: function (canvasId, labels, data) {
            try {
                const el = document.getElementById(canvasId);
                if (!el) return;
                destroyIfExists(canvasId);
                const ctx = el.getContext('2d');
                charts[canvasId] = new Chart(ctx, {
                    type: 'pie',
                    data: {
                        labels: labels || [],
                        datasets: [{
                            data: data || [],
                            backgroundColor: pickColors((labels || []).length),
                            borderColor: 'rgba(255,255,255,0.06)',
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        plugins: {
                            legend: { position: 'right', labels: { boxWidth: 12, padding: 8 } },
                            tooltip: { mode: 'index' }
                        }
                    }
                });
            } catch (e) {
                console.error('chartInterop.renderPie error', e);
            }
        },

        renderBar: function (canvasId, labels, data) {
            try {
                const el = document.getElementById(canvasId);
                if (!el) return;
                destroyIfExists(canvasId);
                const ctx = el.getContext('2d');
                charts[canvasId] = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: labels || [],
                        datasets: [{
                            label: 'Entries',
                            data: data || [],
                            backgroundColor: pickColors((labels || []).length),
                            borderRadius: 6,
                            barThickness: 14
                        }]
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        scales: {
                            x: { grid: { display: false }, ticks: { maxRotation: 0, autoSkip: true, maxTicksLimit: 10 } },
                            y: { beginAtZero: true, ticks: { precision: 0 } }
                        },
                        plugins: {
                            legend: { display: false },
                            tooltip: { mode: 'index' }
                        },
                        layout: { padding: { top: 6, bottom: 6 } }
                    }
                });
            } catch (e) {
                console.error('chartInterop.renderBar error', e);
            }
        },

        destroy: function (canvasId) {
            destroyIfExists(canvasId);
        }
    };
})();