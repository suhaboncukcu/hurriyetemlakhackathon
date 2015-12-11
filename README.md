**Mobile Must have**
> Rest api communication (POST & GET)
> Google maps /X maps
> Search on map


**Superb if we have Mobile**
> Around me function
> Notifications

**REST API Must have**
> latitude
> longitude
> tam adres (text)
> fiyat bilgisi (adres başına) (per id)


From Hurriyet (provider) object
{
	id : unique_id,
	lat : latitude, 
	lng : longitude,
	needPhoto: 1/0,
	declinedPhotoTrialCount : int, 
	pricePerAddress : long int, 
	ownerPhone : +905553449107
}

Technical object additions
{
	createdAt: time(),
	providerId: long int,
	experience : int	
}


photographer database
{
	id : uniqe,
	experience : int,
	name : text, 
	surname: text,

}



**REST API superb if have)
> experience point (defined by declined address photos)


**Tech Stack**
> Unity 
> PhalconPHP REST API
> FirebaseIO





**API Endpoints**

Url:  /api/request

REQ : Object: 
{
	photographerId,
	requestedAddressId,
}

ANS :  Object {
	providerMsg: str,
	phoneNumber: ,
	timer: minutes
}


Url: /api/search
REQ : 
Object {
	lat: ,
	lng: , 
	parameter:,
	address: {
		id : ,
		openAddress:,
		ownerName,
	} 
}



