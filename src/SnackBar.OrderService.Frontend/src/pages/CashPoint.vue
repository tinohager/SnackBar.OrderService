<template>
  <q-page padding>
    Connection: {{ connection.connectionState }}<br>

    <div class="row">
      <div class="col-6 flex">
        <div
          v-for="article in articles"
          :key="article.id"
        >
          <div>
            <q-btn
              outline
              padding="lg"
              class="q-mr-sm"
              :icon="article.icon"
              :label="article.name"
              @click="addToOrder(article)"
            />
          </div>
        </div>
      </div>
      <div class="col-6">
        <div>
          <q-toolbar class="bg-primary text-white shadow-2">
            <q-toolbar-title>Order</q-toolbar-title>
          </q-toolbar>

          <q-list bordered>
            <q-item
              v-for="article in orderArticles"
              :key="article.id"
              class="q-my-sm"
            >
              <q-item-section avatar>
                <q-avatar
                  color="primary"
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
                    color="grey"
                  />
                  <q-btn
                    flat
                    icon="indeterminate_check_box"
                    color="grey"
                  />
                </div>
              </q-item-section>
            </q-item>
          </q-list>
          <q-btn
            v-show="orderArticles.length > 0"
            class="q-mt-sm"
            label="Order"
            @click="sendOrder"
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
  data () {
    return {
      articles: [],
      orderArticles: [],
      connection: null
    }
  },
  async created () {
    this.connection = new HubConnectionBuilder()
      .withUrl('https://localhost:5001/snackbar')
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

      const articleOrder = { id: article.id, name: article.name, amount: 1 }
      this.orderArticles.push(articleOrder)
    },
    sendOrder () {
      this.connection.invoke('CreateOrder', this.orderArticles).catch(function (err) {
        return console.error(err.toString())
      })

      this.orderArticles = []
    }
  }
}
</script>
