const ctxx = document.getElementById('attackChart');


const getdata = document.getElementById('getchartdata');

const values = getdata.dataset.value
    .split(',')
    .map(Number);

new Chart(ctxx, {
    type: 'line',
    data: {
        labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
        datasets: [{
            label: 'Attacks',
            data: values,
            borderColor: '#00e5ff',
            backgroundColor: 'rgba(0,229,255,0.1)',
            tension: 0.4
        }]
    },
    options: {
        plugins: {
            legend: {
                labels: {
                    color: 'white'
                }
            }
        },
        scales: {
            x: {
                ticks: { color: 'white' }
            },
            y: {
                ticks: { color: 'white' }
            }
        }
    }
});