 import {MongoClient} from 'mongodb'
import { useRouter } from 'next/router';

 const handler=async(req,res)=>{
    const router= useRouter();
    if(req.method==='POST'){
        const data=req.body;
        const client=await MongoClient.connect('mongodb+srv://balaji:password%40123@cluster0.k79zwrt.mongodb.net/meetups?retryWrites=true&w=majority')
        const db=client.db();
        const meetupscollection=db.collection('meetups');    
        const result=await meetupscollection.insertOne(data);
        console.log(result._id);
        res.status(201).json({'message':'meetup inserted successfully'});
        router.push('/');
        client.close()
    }
}
export default handler;