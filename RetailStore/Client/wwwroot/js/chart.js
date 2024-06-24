window.drawPieChart = (chartData) => {
    var ctx = document.getElementById('myPieChart').getContext('2d');
    new Chart(ctx, {
        type: 'pie',
        data: chartData,
        options: {
            responsive: true,
            maintainAspectRatio: false
        }
    });
};
}