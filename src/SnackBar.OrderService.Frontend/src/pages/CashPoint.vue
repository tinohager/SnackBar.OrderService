<template>
  <q-page padding>
    <h1>CashPoint</h1>
    <ConnectionStatusBar
      v-if="connection"
      :connection.sync="connection"
    />

    <div class="row">
      <div class="col-6 q-pa-md q-gutter-sm">
        <span
          v-for="article in articles"
          :key="article.id"
        >
          <q-btn
            outline
            padding="lg"
            class="q-mr-sm q-mb-sm"
            :icon="article.icon"
            :label="article.name"
            @click="addToOrder(article)"
          />
        </span>
      </div>
      <div class="col-6">
        <div v-show="orderArticles.length > 0">
          <q-toolbar class="bg-light-green-9 text-white shadow-2">
            <q-toolbar-title>Your Order</q-toolbar-title>
          </q-toolbar>

          <q-list bordered>
            <q-item
              v-for="article in orderArticles"
              :key="article.id"
              class="q-my-sm"
            >
              <q-item-section avatar>
                <q-avatar
                  color="light-green-9"
                  text-color="white"
                  :icon="article.icon"
                />
              </q-item-section>

              <q-item-section>
                <q-item-label>{{ article.name }}</q-item-label>
              </q-item-section>

              <q-item-section>
                {{ article.amount }}
              </q-item-section>

              <q-item-section
                side
              >
                <div class="text-grey-8 q-gutter-xs">
                  <q-btn
                    flat
                    icon="add_box"
                    size="xl"
                    color="grey"
                    @click="increaseOrderAmount(article)"
                  />
                  <q-btn
                    flat
                    icon="indeterminate_check_box"
                    size="xl"
                    color="grey"
                    @click="decreaseOrderAmount(article)"
                  />
                </div>
              </q-item-section>
            </q-item>
          </q-list>
          <q-btn
            push
            class="q-mt-sm"
            color="light-green-9"
            label="Order"
            size="xl"
            @click="sendOrder"
          />
          <q-btn
            push
            class="q-mt-sm q-ml-sm"
            color="grey-6"
            label="Cancel"
            size="xl"
            @click="cancelOrder"
          />
        </div>
      </div>
    </div>
  </q-page>
</template>

<script>
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'

export default {
  name: 'CashPoint',
  components: {
    ConnectionStatusBar: () => import('../components/ConnectionStatusBar.vue')
  },
  data () {
    return {
      articles: [],
      orderArticles: [],
      connection: null
    }
  },
  async created () {
    this.connection = new HubConnectionBuilder()
      .withUrl('hub/order')
      .configureLogging(LogLevel.Information)
      .withAutomaticReconnect()
      .build()

    this.connection.start()

    await this.getArticles()
  },
  beforeDestroy () {
    this.connection.stop()
  },
  methods: {
    async getArticles () {
      try {
        const response = await this.$axios.get('/api/Article')
        this.articles = response.data
      } catch (error) {
        this.$q.notify({
          type: 'negative',
          message: 'Request failure',
          caption: `${error}`
        })
      }
    },
    addToOrder (article) {
      const exists = this.orderArticles.find(item => item.id === article.id)
      if (exists) {
        exists.amount++
        return
      }

      const articleOrder = {
        id: article.id,
        name: article.name,
        icon: article.icon,
        amount: 1
      }

      this.orderArticles.push(articleOrder)
    },
    async sendOrder () {
      try {
        this.connection.invoke('CreateOrder', this.orderArticles)
      } catch (error) {
        this.$q.notify({
          type: 'negative',
          message: 'Order error',
          group: false,
          classes: 'text-h4'
        })
        return
      }

      this.$q.notify({
        type: 'positive',
        message: 'Order successful',
        color: 'light-green-9',
        group: false,
        classes: 'text-h4'
      })

      this.orderArticles = []
    },
    cancelOrder () {
      this.orderArticles = []
    },
    decreaseOrderAmount (article) {
      article.amount--

      if (article.amount === 0) {
        const index = this.orderArticles.indexOf(article)
        if (index > -1) {
          this.orderArticles.splice(index, 1)
        }
      }
    },
    increaseOrderAmount (article) {
      article.amount++
    }
  }
}
</script>
