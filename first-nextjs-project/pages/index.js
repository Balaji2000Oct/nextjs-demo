import MeetupList from "../components/meetups/MeetupList";
import { MongoClient } from "mongodb";

import {Fragment} from 'react'
const HomePage = props => {
  return ( <Fragment>
     
      <MeetupList meetups={props.meetups} />
      </Fragment>);
 
};
export const getStaticProps = async () => {
  const client = await MongoClient.connect(
    "mongodb+srv://balaji:password%40123@cluster0.k79zwrt.mongodb.net/meetups?retryWrites=true&w=majority"
  );
  const db = client.db();
  const meetupscollection = db.collection("meetups");
  const meetups = await meetupscollection.find().toArray();
  return {
    props: {
      meetups: meetups.map(item => ({
        id: item._id.toString(),
        title: item.title,
        address: item.address,
        description: item.description,
        image: item.image
      }))
    },
    revalidate: 1
  };
};
export default HomePage;
