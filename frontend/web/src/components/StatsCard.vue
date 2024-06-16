<template>
  <div class="row">
    <div><bar :data="barData" :options="options" /></div>
    <div><doughnut :data="data" :options="options" /></div>
  </div>
</template>

<script setup lang="ts">
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  ArcElement,
  CategoryScale,
  LinearScale,
} from 'chart.js';
import { Doughnut, Bar } from 'vue-chartjs';

ChartJS.register(
  ArcElement,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);

const options = {
  responsive: true,
  maintainAspectRatio: false,
};

import { Carotte } from 'src/models/Carotte';
import { useCarotteStore } from 'src/stores/carotte-store';
const carotteStore = useCarotteStore();

const isToday = (dateStr: string): boolean => {
  const date = new Date(dateStr);
  const today = new Date();
  return date.toDateString() === today.toDateString();
};
const isThisWeek = (dateStr: string): boolean => {
  const date = new Date(dateStr);
  const today = new Date();
  const startOfWeek = new Date(today.setDate(today.getDate() - today.getDay())); // Set to Sunday of the current week
  const endOfWeek = new Date(startOfWeek);
  endOfWeek.setDate(endOfWeek.getDate() + 6); // Saturday of the current week

  return date >= startOfWeek && date <= endOfWeek;
};

const getHistoryStats = (carottes: Carotte[]) => {
  let todayCount = 0;
  let todayCountReward = 0;
  let todayCountNotReward = 0;
  let thisWeekCount = 0;
  const detailedHistory: Record<string, string[]> = {};

  for (const carotte of carottes) {
    if (carotte.history) {
      for (const dateStr of carotte.history) {
        if (isToday(dateStr)) {
          todayCount++;
          if (carotte.isReward) todayCountReward += carotte.points ?? 0;
          else todayCountNotReward += carotte.points ?? 0;
        }
        if (isThisWeek(dateStr)) {
          thisWeekCount++;
        }
      }
      detailedHistory[carotte.id ?? 'unknown'] = carotte.history;
    }
  }
  return {
    todayCount,
    thisWeekCount,
    detailedHistory,
    todayCountReward,
    todayCountNotReward,
  };
};

const stats = getHistoryStats(carotteStore.carottes);

function isWithinLast7Days(dateStr: string): boolean {
  const date = new Date(dateStr);
  const today = new Date();
  const sevenDaysAgo = new Date(today);
  sevenDaysAgo.setDate(today.getDate() - 7);

  return date >= sevenDaysAgo && date <= today;
}

// Function to get points earned per day over the last 7 days, split by isReward
function getPointsLast7Days(carottes: Carotte[]) {
  const pointsPerDay: Record<string, { reward: number; nonReward: number }> =
    {};

  for (const carotte of carottes) {
    if (carotte.history && carotte.points !== undefined) {
      for (const dateStr of carotte.history) {
        if (isWithinLast7Days(dateStr)) {
          const dateKey = new Date(dateStr).toISOString().split('T')[0]; // Format YYYY-MM-DD

          if (!pointsPerDay[dateKey]) {
            pointsPerDay[dateKey] = { reward: 0, nonReward: 0 };
          }

          if (carotte.isReward) {
            pointsPerDay[dateKey].reward += carotte.points;
          } else {
            pointsPerDay[dateKey].nonReward += carotte.points;
          }
        }
      }
    }
  }

  return pointsPerDay;
}

const stats2 = getPointsLast7Days(carotteStore.carottes);

console.log(stats2);

console.log('stats:', stats);

const barData = {
  labels: [
    'January',
    'February',
    'March',
    'April',
    'May',
    'June',
    'July',
    'August',
    'September',
    'October',
    'November',
    'December',
  ],
  datasets: [
    {
      label: 'Good',
      backgroundColor: 'green',
      data: [40, 20, 12, 39, 10, 40, 39, 80, 40, 20, 12, 11],
    },
    {
      label: 'Bad',
      backgroundColor: 'red',
      data: [40, 20, 12, 39, 10, 40, 39, 80, 40, 20, 12, 11],
    },
  ],
};
const data = {
  labels: ['Good', 'Bad'],
  datasets: [
    {
      backgroundColor: ['green', 'red'],
      data: [stats.todayCountReward + 5, stats.todayCountNotReward + 25],
    },
  ],
};
console.log(stats.todayCountReward)
</script>

<style scoped></style>
