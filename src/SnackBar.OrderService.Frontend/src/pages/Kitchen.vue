<template>
  <q-page padding>
    Connection: {{ connection.connectionState }}<br>

    <div
      v-for="order in orders"
      :key="order"
      class="q-mb-sm"
    >
      <q-list bordered>
        <q-item
          v-for="article in order"
          :key="article.id"
          v-ripple
          class="q-my-sm"
        >
          <q-item-section>
            <q-item-label>{{ article.name }}</q-item-label>
          </q-item-section>

          <q-item-section>
            {{ article.amount }}
          </q-item-section>
        </q-item>
      </q-list>
    </div>
  </q-page>
</template>

<script>
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'

export default {
  name: 'Kitchen',
  data () {
    return {
      connection: null,
      orders: []
    }
  },
  created () {
    this.connection = new HubConnectionBuilder()
      .withUrl('https://localhost:5001/snackbar')
      .configureLogging(LogLevel.Information)
      .withAutomaticReconnect()
      .build()

    this.connection.start()

    this.connection.on('NewOrder', order => {
      this.orders.push(order)
    })
  },
  beforeDestroy () {
    this.connection.stop()
  }
}
</script>
