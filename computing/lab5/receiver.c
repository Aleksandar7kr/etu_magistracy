#include <mqueue.h>
#include <stdio.h>
#include <stdlib.h>
#include <errno.h>
#include <time.h>

#include "message.h"

int main(int argc, char** argv) {
	mqd_t qid = mq_open("/mailbox", O_RDONLY);

	if(qid == -1) {
		perror("mq_open");
		return 1;
	}

	do {
		int priority = 0;
		struct mq_attr attr;
		message_t msg;
		
		if(attr.mq_curmsgs == 0)
		{
			printf("receiver: died because of starvation\n");
			return 1;
		}

		if(mq_receive(qid, (char *) &msg, sizeof(message_t), &priority) == -1) {
			perror("mq_receive");
			return 1;
		}

		printf("receiver: message received, cmd == %d, arg == %d\n",
			msg.command, msg.argument);

		if(msg.command == CMD_KILL)
			break;

		if(msg.command == CMD_WAIT)
		{
			sleep(msg.argument); 
			mq_getattr(qid, &attr);
			continue;
		}

		printf("receiver: unknown command code (%d)\n", msg.command);
		return 1;

	} while(1);

	return 0;
}