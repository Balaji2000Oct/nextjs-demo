import MeetupDetail from '../../components/meetups/MeetupDetail'
import {MongoClient,ObjectId} from 'mongodb'
const MeetupDetails=(props)=>{
    return(
       
             <MeetupDetail 
             address={props.meetupData.address} 
             image={props.meetupData.image}
             title={props.meetupData.title}
             description={props.meetupData.description}/>
    );
};
export const getStaticPaths=async()=>{
    const client = await MongoClient.connect(
        "mongodb+srv://balaji:password%40123@cluster0.k79zwrt.mongodb.net/meetups?retryWrites=true&w=majority"
      );
      const db = client.db();
      const meetupscollection = db.collection("meetups");
     const meetups=await meetupscollection.find({},{_id:1}).toArray();
     client.close();
    return({
        fallback:false,
        paths:meetups.map(item=>({
            params:{meetupId:item._id.toString()}
        }))
    })
};
export const getStaticProps=async(context)=>{
    const meetupId=context.params.meetupId;
    const client = await MongoClient.connect(
        "mongodb+srv://balaji:password%40123@cluster0.k79zwrt.mongodb.net/meetups?retryWrites=true&w=majority"
      );
      const db = client.db();
      const meetupscollection = db.collection("meetups");
      const item=await meetupscollection.findOne({_id:ObjectId(meetupId)});
       //console.log(meetup)
      client.close();
      return({
          props:{
              meetupData:
                  {
                      id:item._id.toString(),
                      image:item.image,
                      title:item.title,
                      description:item.description,
                      address:item.address
                  }
              
          }
      })

};
export default MeetupDetails;