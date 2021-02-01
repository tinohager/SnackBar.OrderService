<template>
  <q-page padding>
    <h1>Kitchen</h1>
    <ConnectionStatusBar
      v-if="connection"
      :connection="connection"
    />

    <div
      v-for="order in orders"
      :key="order"
      class="q-mb-sm"
    >
      <q-slide-item
        right-color="negative"
        @right="removeOrder(order)"
      >
        <template v-slot:right>
          <q-icon name="delete" />
        </template>

        <q-item>
          <q-item-section avatar>
            <q-avatar
              color="negative"
              text-color="white"
              icon="delete"
            />
          </q-item-section>
          <q-item-section>Remove order</q-item-section>
        </q-item>
      </q-slide-item>
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
  components: {
    ConnectionStatusBar: () => import('../components/ConnectionStatusBar.vue')
  },
  data () {
    return {
      connection: null,
      orders: []
    }
  },
  created () {
    this.connection = new HubConnectionBuilder()
      .withUrl('hub/order')
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
  },
  methods: {
    removeOrder (order) {
      const index = this.orders.indexOf(order)
      if (index > -1) {
        this.orders.splice(index, 1)
      }
    }
  }
}
</script>
