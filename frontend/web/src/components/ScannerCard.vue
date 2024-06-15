<template>
  <div>
    Scanner ci-dessous:
    <StreamBarcodeReader 
      @decode="onDecode"
      @loaded="onLoaded"
    ></StreamBarcodeReader>
  </div>
</template>

<script setup lang="ts">
import { StreamBarcodeReader } from 'vue-barcode-reader';
const onDecode = () => {
  console.log('onDecode');
};
const onLoaded = () => {
  console.log('onLoaded');
};
</script>

<style scoped></style>
