import '../styles/globals.css'
import Head from 'next/head'
import Layout from '../components/layout/Layout'
function MyApp({ Component, pageProps }) {
  return <Layout>
     <Head>
       <title>React Meetups</title>
       <meta name='description' content='A huge number of highly react meetups!'/>
      </Head>
    <Component {...pageProps} /></Layout>
}

export default MyApp
